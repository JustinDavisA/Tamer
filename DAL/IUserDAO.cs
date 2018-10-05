using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUserDAO
    {
        List<UserDM> GetUser();
        void AddUser(UserDM user);
        void UpdateUser(UserDM user);
        void RemoveUser(int id);
        UserDM GetUserById(int id);
        UserDM CompareLogin(string userName, string password);
        UserDM GetUserByUsername(string userName);
    }
}
