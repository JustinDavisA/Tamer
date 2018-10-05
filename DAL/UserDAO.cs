using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLogger;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class UserDAO : IUserDAO
    {
        private ILoggerIO logger;
        private IDAO dataWriter;
        string connectionString = @"Server=GDC-BC-013\SQLEXPRESS;Database = TamerDatabase;Trusted_Connection=True;";

        public UserDAO(IDAO data, ILoggerIO log)
        {
            dataWriter = data;
            logger = log;
        }
        public List<UserDM> ReadUser(SqlParameter[] parameters, string statement)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    SqlDataReader data = command.ExecuteReader();
                    List<UserDM> users = new List<UserDM>();
                    while (data.Read())
                    {
                        UserDM user = new UserDM();
                        user.Id = Convert.ToInt32(data["Id"]);
                        user.Username = data["Username"].ToString();
                        user.Password = data["Password"].ToString();
                        user.Role = data["Role"].ToString();
                        users.Add(user);
                    }
                    return users;
                }
            }
        }
        public List<UserDM> GetUser()
        {
            try
            {
                logger.LogError("Event", "The user was able to fetch users", "Class: UserDAO & Method Name: GetUser");
                return ReadUser(null, "GetUsers");
            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to fetch users", "Class: UserDAO & Method Name: GetUser");
                return null;
            }
        }
        public void AddUser(UserDM user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username",user.Username)
                ,new SqlParameter("@Password",user.Password)
                ,new SqlParameter("@Role",user.Role)
            };
            try
            {
                dataWriter.Write(parameters, "CreateUser");
                logger.LogError("Event", "The user was able to add a user", "Class: UserDAO & Method Name: AddUser");

            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to add a user", "Class: UserDAO & Method Name: AddUser");

            }
        }
        public void UpdateUser(UserDM user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",user.Id)
                ,new SqlParameter("@Username",user.Username)
                ,new SqlParameter("@Password",user.Password)
                ,new SqlParameter("@Role",user.Role)
            };
            try
            {
                dataWriter.Write(parameters, "UpdateUser");
                logger.LogError("Event", "The user was able to update a user", "Class: UserDAO & Method Name: UpdateUser");

            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to update a user", "Class: UserDAO & Method Name: UpdateUser");
            }
        }
        public void RemoveUser(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            };
            try
            {
                dataWriter.Write(parameters, "DeleteUser");
                logger.LogError("Event", "The user was able to remove a user", "Class: UserDAO & Method Name: RemoveUser");
            }
            catch (Exception e)
            {
                logger.LogError("Error", "The user was not able to remove a user", "Class: UserDAO & Method Name: RemoveUser");
            }
        }
        public UserDM GetUserById(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
                 {
                     new SqlParameter("@Id",id)
                 };
            return ReadUser(parameters, "GetUserById")[0];
        }
        public UserDM CompareLogin(string username, string password)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username)
                ,new SqlParameter("@Password", password)
            };
            return ReadUser(parameters, "CompareLogin")[0];
        }
        public UserDM GetUserByUsername(string username)
        {
            SqlParameter[] parameters = new SqlParameter[]
                 {
                     new SqlParameter("@Username",username)
                 };
            return ReadUser(parameters, "GetUserByUsername")[0];
        }
    }
}
