using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using ApplicationLogger;
using Tamer.Models;

namespace Tamer.Controllers
{
    public class UserController : Controller
    {
        private IUserLogic userLogic;
        private ILoggerIO log;
        public UserController(IUserLogic logic, ILoggerIO logger)
        {
            userLogic = logic;
            log = logger;
        }
        // GET: User
        public ActionResult Index()
        {
            List<UserViewModel> users = UserViewModel.Map(userLogic.GetUser());
            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View(UserViewModel.Map(userLogic.GetUserById(id)));
        }

        // GET: User/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Register(UserViewModel user)
        {
            try
            {
                string redirectLocation = userLogic.CreateUserRedirect(UserViewModel.Map(user), (string)Session["Role"], user.ConfirmPassword);
                if (redirectLocation != "That user already exists" && redirectLocation != "Your passwords do not match")
                {
                    // TODO: Add insert logic here
                    return RedirectToAction(redirectLocation);
                }
                else
                {
                    ViewBag.RegisterMessage = redirectLocation;
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View(UserViewModel.Map(userLogic.GetUserById(id)));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserViewModel user, string roleDropDown)
        {
            try
            {
                // TODO: Add update logic here
                user.Role = roleDropDown;
                string msg = userLogic.EditByRole((string)Session["Role"], UserViewModel.Map(user), user.OldPassword, user.NewPassword, user.ConfirmPassword);
                ViewBag.EditMessage = msg;
                if ((string)ViewBag.EditMessage == "Congratulations! You successfully changed your password")
                {
                    return RedirectToAction("Index", "Monster");
                }
                else if ((string)ViewBag.EditMessage == "You have successfully updated user.")
                {
                    return RedirectToAction("Index", "User");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult AdminEdit(int id)
        {
            return View(UserViewModel.Map(userLogic.GetUserById(id)));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult AdminEdit(int id, UserViewModel user, string roleDropDown)
        {
            try
            {
                // TODO: Add update logic here
                user.Role = roleDropDown;
                string msg = userLogic.EditByRole((string)Session["Role"], UserViewModel.Map(user), null, user.NewPassword, user.ConfirmPassword);
                ViewBag.EditMessage = msg;
                if ((string)ViewBag.EditMessage == "You have successfully updated the user!")
                {
                    return RedirectToAction("Index", "User");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View(UserViewModel.Map(userLogic.GetUserById(id)));
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, UserViewModel person)
        {
            try
            {
                // TODO: Add delete logic here
                userLogic.RemoveUser(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: User/Login
        public ActionResult Login()
        {
            return View();
        }
        //POST: User/Login
        [HttpPost]
        public ActionResult Login(UserViewModel person)
        {
            try
            {
                if (userLogic.Login(person.Username, person.Password))
                {
                    person = UserViewModel.Map(userLogic.GetUserByUsernameAndPassword(person.Username, person.Password));
                    Session["LoggedIn"] = true;
                    Session["UserName"] = person.Username;
                    Session["UserId"] = person.Id;
                    Session["Role"] = person.Role;
                    return RedirectToAction("Index", "Monster");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }
        // GET: User/Logout
        public ActionResult Logout()
        {
            Session["LoggedIn"] = null;
            return RedirectToAction("Index", "Home");
        }
        // GET: User/AccessDenied
        public ActionResult AccessDenied()
        {
            return View();
        }

    }
}
