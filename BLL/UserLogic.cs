using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLogger;
using DAL;

namespace BLL
{
    public class UserLogic : IUserLogic
    {
        private ILoggerIO logger;
        private IUserDAO userData;
        private IHash userHash;
        public List<User> userLogins = new List<User>();

        public UserLogic(IUserDAO data, IHash hashPassword, ILoggerIO log)
        {
            userHash = hashPassword;
            userData = data;
            logger = log;
        }
        public List<User> GetUser()
        {
            try
            {
                ReadUser();
                logger.LogError("Event", "The user was able to read a user", "Class: UserLogic & Method Name: GetUser");
                return userLogins;
            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to read user", "Class: UserLogic & Method Name: GetUser");
                return null;
            }
        }
        //Actually private TEST AS public
        public void ReadUser()
        {
            List<UserDM> user = userData.GetUser();
            foreach (UserDM person in user)
            {
                userLogins.Add(Map(person));
                logger.LogError("Event", "The user was able to read user", "Class: UserLogic & Method Name: GetUser");
            }
        }
        public void CreateUser(User user)
        {
            try
            {
                user.Password = userHash.GetHash(user.Password);
                user.Role = "User";
                userData.AddUser(Map(user));
                logger.LogError("Event", "The user was able to create an user", "Class: UserLogic & Method Name: CreateUser");
            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was able not to create an user", "Class: UserLogic & Method Name: CreateUser");
            }
        }
        public User Map(UserDM user)
        {
            User person = new User();
            person.Id = user.Id;
            person.Username = user.Username;
            person.Password = user.Password;
            person.Role = user.Role;
            return person;
        }
        public UserDM Map(User user)
        {
            UserDM person = new UserDM();
            person.Id = user.Id;
            person.Username = user.Username;
            person.Password = user.Password;
            person.Role = user.Role;
            return person;
        }
        public void UpdateUser(User user)
        {
            userData.UpdateUser(Map(user));
        }
        public void RemoveUser(int id)
        {
            try
            {
                userData.RemoveUser(id);
                logger.LogError("Event", "The user was able to remove a user", "Class: UserLogic & Method Name: RemoveUser");

            }
            catch
            {
                logger.LogError("Error", "The user was able not to remove a user", "Class: UserLogic & Method Name: RemoveUser");
            }
        }
        public bool UserExistCheck(string login)//checks if username exists in user list //logic
        {
            try
            {
                if (userData.GetUserByUsername(login).Username != null)
                {
                    logger.LogError("Event", "The user was able to check for user", "Class: UserLogic & Method Name: UserExistCheck");
                    return true;
                }
                logger.LogError("Event", "The user was able to check for user", "Class: UserLogic & Method Name: UserExistCheck");
                return false;
            }
            catch
            {
                logger.LogError("Error", "The user was not able to check for user", "Class: UserLogic & Method Name: UserExistCheck");
                return false;
            }
        }
        public bool Login(string username, string password) //logic
        {
            try
            {
                if (userData.CompareLogin(username, userHash.GetHash(password)).Username != null)
                {
                    logger.LogError("Event", "The user was able to login", "Class: UserLogic & Method Name: Login");
                    return true;
                }
                logger.LogError("Event", "The user was able to login", "Class: UserLogic & Method Name: Login");
                return false;
            }
            catch
            {
                logger.LogError("Error", "The user was not able to login", "Class: UserLogic & Method Name: Login");
                return false;
            }
        }
        public User GetUserById(int id)
        {
            try
            {
                logger.LogError("Event", "The user was able to get the user by ID", "Class: UserLogic & Method Name:GetUserById");
                return Map(userData.GetUserById(id));
            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to get the user by ID", "Class: UserLogic & Method Name:GetUserById");
                return null;
            }
        }
        public User GetUserByUsernameAndPassword(string username, string password)
        {
            try
            {
                logger.LogError("Event", "The user was able to get the user by their user name and password", "Class: UserLogic & Method Name:GetUserByUserNameAndPassword");
                return Map(userData.CompareLogin(username, userHash.GetHash(password)));
            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to get the user by their user name and password", "Class: UserLogic & Method Name:GetUserByUserNameAndPassword");
                return null;
            }
        }
        public string EditPassword(User person, string oldPassword, string newPassword, string confirmPassword)
        {
            if (newPassword == confirmPassword && Login(GetUserById(person.Id).Username, oldPassword))
            {
                person.Password = userHash.GetHash(newPassword);
                person.Role = GetUserById(person.Id).Role;
                UpdateUser(person);
                return "You've successfully changed your password!";
            }
            else if (newPassword != confirmPassword)
            {
                return "Your passwords do not match. Please try again.";
            }
            return "Your original password doesn't match. Please try again.";
        }
        public string EditUser(User person, string newPassword, string confirmPassword)
        {
            if (newPassword == null)
            {
                person.Password = GetUserById(person.Id).Password;
                return UpdateUserAndReturnMessage(person);
            }
            else if (newPassword == confirmPassword)
            {
                person.Password = userHash.GetHash(newPassword);
                return UpdateUserAndReturnMessage(person);
            }
            return "Your passwords do not match. Please try again.";
        }
        public string UpdateUserAndReturnMessage(User person)
        {
            UpdateUser(person);
            return "You have successfully updated the user!";
        }
        public string EditByRole(string currentRole, User user, string oldPassword, string newPassword, string confirmPassword)
        {
            if (currentRole == "Admin")
            {
                return EditUser(user, newPassword, confirmPassword);
            }
            else
            {
                return EditPassword(user, oldPassword, newPassword, confirmPassword);
            }
        }
        public string CreateUserRedirect(User person, string currentRole, string confirmPassword)
        {
            if (person.Password == confirmPassword && !UserExistCheck(person.Username) && (currentRole != "Admin"))
            {
                return CreateUserReturnLocation(person, "Login");
            }
            else if (person.Password == confirmPassword && !UserExistCheck(person.Username))
            {
                return CreateUserReturnLocation(person, "Index");
            }
            else if (UserExistCheck(person.Username))
            {
                return "That user already exists";
            }
            return "Your passwords do not match";
        }

        public string CreateUserReturnLocation(User person, string location)
        {
            CreateUser(person);
            return location;
        }
    }
}
