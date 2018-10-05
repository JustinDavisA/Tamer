using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using DAL;
using ApplicationLogger;
using Rhino.Mocks;
using Tamer.Models;
using Tamer.Controllers;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Tamer.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private IUserLogic logic { get; set; }
        private ILoggerIO logger { get; set; }
        private List<User> MockUsers = new List<User>()
        {
          new User {Id=1,Username="Mac",Password="Pass1", Role="true"},
          new User {Id=2,Username="And",Password="Pass1", Role="false"},
          new User {Id=3,Username="Cheese",Password="Pass1", Role="false"}
        };
        private User MockUser = new User();
        private UserViewModel MockUser2 = new UserViewModel();
        private UserController getController()
        {
            logic = MockRepository.GenerateStub<IUserLogic>();
            UserController uController = new UserController(logic, logger);
            return uController;
        }
        [TestMethod]
        public void TestIndex()
        {
            //Arrange:
            UserController controller = getController();
            logic.Expect(L => L.GetUser()).Return(MockUsers);
            //Act:
            ViewResult result = controller.Index() as ViewResult;
            //Assert:
            logic.VerifyAllExpectations();
        }
        [TestMethod]
        public void TestDetails()
        {
            //Arrange:
            UserController controller = getController();
            int id = 4;
            logic.Expect(L => L.GetUserById(id)).Return(MockUsers[0]);
            //Act:
            ViewResult result = controller.Details(id) as ViewResult;
            //Assert:
            logic.VerifyAllExpectations();
        }
        [TestMethod]
        public void TestRegisterGet()
        {
            //Arrange:
            UserController controller = getController();
            //Act:
            ViewResult result = controller.Register() as ViewResult;
            //Assert:
            logic.VerifyAllExpectations();
        }
        [TestMethod]
        public void TestRegisterPost()
        {
            //Arrange:
            UserController controller = getController();
            string currentRole = "Admin";
            string confirmPassword = "jimmy";
            logic.Expect(L => L.CreateUserRedirect(MockUsers[0], currentRole, confirmPassword)).Return(currentRole);
            //Act:
            ViewResult result = controller.Register(MockUser2) as ViewResult;
            //Assert:
            logic.VerifyAllExpectations();
        }
        //[TestMethod]
        //public void TestEditGet()
        //{
        //    //Arrange:
        //    UserController controller = getController();
        //    int id = 3;
        //    logic.Expect(L => L.GetUserById(id)).Return(id);
        //    //Act:
        //    ViewResult result = controller.Register() as ViewResult;
        //    //Assert:
        //    logic.VerifyAllExpectations();
        //}
    }
}
