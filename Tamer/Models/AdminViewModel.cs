using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using System.ComponentModel.DataAnnotations;

namespace Tamer.Models
{
    public class AdminViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        public static AdminViewModel Map(User person)
        {
            AdminViewModel user = new AdminViewModel();
            user.Id = person.Id;
            user.UserName = person.Username;
            user.Password = person.Password;
            user.Role = person.Role;
            return user;
        }
        public static User Map(AdminViewModel person)
        {
            User user = new User();
            user.Id = person.Id;
            user.Username = person.UserName;
            user.Password = person.Password;
            user.Role = person.Role;
            return user;
        }
        public static List<AdminViewModel> Map(List<User> people)
        {
            List<AdminViewModel> users = new List<AdminViewModel>();
            foreach (User user in people)
            {
                users.Add(Map(user));
            }
            return users;
        }
        public static List<User> Map(List<AdminViewModel> people)
        {
            List<User> users = new List<User>();
            foreach (AdminViewModel user in people)
            {
                users.Add(Map(user));
            }
            return users;
        }
    }
}

