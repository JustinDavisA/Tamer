using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IUserLogic
    {
        List<User> GetUser();
        void ReadUser();
        void CreateUser(User user);
        void UpdateUser(User user);
        void RemoveUser(int id);
        //void EditPassword(string username, string newPass);
        bool UserExistCheck(string login);
        bool Login(string userName, string password);
        User GetUserById(int id);
        User GetUserByUsernameAndPassword(string userName, string password);
        string EditPassword(User person, string oldPassword, string newPassword, string confirmPassword);
        string EditUser(User person, string newPassword, string confirmPassword);
        string UpdateUserAndReturnMessage(User person);
        string EditByRole(string currentRole, User user, string oldPassword, string newPassword, string confirmPassword);
        string CreateUserRedirect(User person, string currentRole, string confirmPassword);
    }
}
