using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using System.ComponentModel.DataAnnotations;

namespace Tamer.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        //[RegularExpression("((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()-_=+]).{6,20})", ErrorMessage = "Password must include capital letter, lowercase letter, a number, and a special character")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Original Password")]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        //[RegularExpression("((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()-_=+]).{6,20})", ErrorMessage = "Password must include capital letter, lowercase letter, a number, and a special character")]
        public string NewPassword { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }

        public static UserViewModel Map(User person)
        {
            UserViewModel user = new UserViewModel();
            user.Id = person.Id;
            user.Username = person.Username;
            user.Password = person.Password;
            user.Role = person.Role;
            return user;
        }
        public static User Map(UserViewModel person)
        {
            User user = new User();
            user.Id = person.Id;
            user.Username = person.Username;
            user.Password = person.Password;
            user.Role = person.Role;
            return user;
        }
        public static List<UserViewModel> Map(List<User> people)
        {
            List<UserViewModel> users = new List<UserViewModel>();
            foreach (User user in people)
            {
                users.Add(Map(user));
            }
            return users;
        }
        public static List<User> Map(List<UserViewModel> people)
        {
            List<User> users = new List<User>();
            foreach (UserViewModel user in people)
            {
                users.Add(Map(user));
            }
            return users;
        }
    }
}