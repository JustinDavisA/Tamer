using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using DAL;
using ApplicationLogger;
using Rhino.Mocks;
namespace Tamer.Tests.BLL
{
    [TestClass]
    public class UserLogicTest
    {
        IHash hashData = MockRepository.GenerateMock<IHash>();
        ILoggerIO logData = MockRepository.GenerateMock<ILoggerIO>();
        IUserDAO userData = MockRepository.GenerateMock<IUserDAO>();

        private List<User> MockUsers1 = new List<User>()
        {
            new User {Id=1,Username="Ted",Password="Meow1",Role="Admin"},
            new User {Id=2,Username="Tristan",Password="Meow2",Role="Poweruser"},
            new User {Id=3,Username="Cody",Password="Meow3",Role="User"}
        };
        private List<UserDM> MockUsers2 = new List<UserDM>()
        {
            new UserDM {Id=1,Username="Malcolm",Password="Meow1",Role="Admin"},
            new UserDM {Id=2,Username="Tristan",Password="Meow2",Role="Poweruser"},
            new UserDM {Id=3,Username="Cody",Password="Meow3",Role="User"}
        };
        private List<MessageDO> MockMessages = new List<MessageDO>()
        {
            new MessageDO {DateTime="6/4/15 08:55:00",ErrorType="Event",ErrorMessage="A New User has been added to the database", Layer="Class:UserLogic, Method:CreateUser"},
            new MessageDO {DateTime="6/4/15 08:55:00",ErrorType="Error",ErrorMessage="A New User has not been added to the database", Layer="Class:UserLogic, Method:CreateUser"}
        };
        //Tests both methods as ReadUser is not directly testable
        [TestMethod]
        public void GetUserTestAndReadUserCall()
        {
            //Arrange
            userData.Expect(mock => mock.GetUser()).Return(MockUsers2);
            logData.Expect(mock => mock.LogError("Event", "The user was able to read a user", "Class: UserLogic & Method Name: GetUser"));
            UserLogic target = new UserLogic(userData, hashData, logData);
            //Act
            List<User> users = target.GetUser();
            //Assert
            userData.VerifyAllExpectations();
            logData.VerifyAllExpectations();
            Assert.IsNotNull(users);
            Assert.AreEqual(MockUsers2[0].Username, users[0].Username);
        }
        [TestMethod]
        public void CreateUserTest()
        {
            //Arrange
            User user = MockUsers1[0];
            UserDM user2 = MockUsers2[0];
            userData.Expect(mock => mock.AddUser(Arg<UserDM>.Is.Anything));
            logData.Expect(mock => mock.LogError("Event", "The user was able to create an user", "Class: UserLogic & Method Name: CreateUser"));
            UserLogic target = new UserLogic(userData, hashData, logData);
            //Act
            target.CreateUser(user);
            //Assert
            userData.VerifyAllExpectations();
            logData.VerifyAllExpectations();
        }
        [TestMethod]
        public void Map_UserToUserDM_Test()
        {
            //Arrange
            User user = MockUsers1[0];
            UserDM user2 = new UserDM();
            UserLogic target = new UserLogic(userData, hashData, logData);
            //Act
            user2 = target.Map(user);
            //Assert
            Assert.IsNotNull(user2);
            Assert.AreEqual(user.Username, user2.Username);
            Assert.AreEqual(user.Password, user2.Password);
            Assert.AreEqual(user.Id, user2.Id);
        }
        [TestMethod]
        public void Map_UserDMToUser_Test()
        {
            //Arrange
            User user = new User();
            UserDM user2 = MockUsers2[0];
            UserLogic target = new UserLogic(userData, hashData, logData);
            //Act
            user2 = target.Map(user);
            //Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Username, user2.Username);
            Assert.AreEqual(user.Password, user2.Password);
            Assert.AreEqual(user.Id, user2.Id);
        }
        [TestMethod]
        public void UpdateUserTest()
        {
            //Arrange
            User user = MockUsers1[0];
            userData.Expect(mock => mock.UpdateUser(Arg<UserDM>.Is.Anything));
            UserLogic target = new UserLogic(userData, hashData, logData);
            //Act
            target.UpdateUser(user);
            //Assert
            userData.VerifyAllExpectations();
        }
        [TestMethod]
        public void RemoveUserTest()
        {
            //Arrange
            int id = 1;
            userData.Expect(mock => mock.RemoveUser(1));
            logData.Expect(mock => mock.LogError("Event", "The user was able to remove a user", "Class: UserLogic & Method Name: RemoveUser"));
            UserLogic target = new UserLogic(userData, hashData, logData);
            //Act
            target.RemoveUser(id);
            //Assert
            userData.VerifyAllExpectations();
            logData.VerifyAllExpectations();
        }
        [TestMethod]
        public void UserExistCheckTest()
        {
            //Arrange
            string login = "Sarah";
            userData.Expect(mock => mock.GetUserByUsername(login)).Return(new UserDM());
            logData.Expect(mock => mock.LogError("Event", "The user was able to check for user", "Class: UserLogic & Method Name: UserExistCheck"));
            UserLogic target = new UserLogic(userData, hashData, logData);
            //Act
            bool exists = target.UserExistCheck(login);
            //Assert
            userData.VerifyAllExpectations();
            logData.VerifyAllExpectations();
            Assert.IsFalse(exists);
        }
        [TestMethod]
        public void LoginTest()
        {
            //Arrange
            string username = "Sarah";
            string password = "Doop";
            hashData.Expect(mock => mock.GetHash(password)).Return("Totally a hashed password");
            userData.Expect(mock => mock.CompareLogin(Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Return(new UserDM());
            logData.Expect(mock => mock.LogError("Event", "The user was able to login", "Class: UserLogic & Method Name: Login"));
            UserLogic target = new UserLogic(userData, hashData, logData);
            //Act
            bool success = target.Login(username, password);
            //Assert
            userData.VerifyAllExpectations();
            logData.VerifyAllExpectations();
            hashData.VerifyAllExpectations();
            Assert.IsFalse(success);
        }
        [TestMethod]
        public void GetUserByIdTest()
        {
            //Arrange:
            int id = 1;
            userData.Expect(mock => mock.GetUserById(id));
            logData.Expect(mock => mock.LogError("Event", "The user was able to get the user by ID", "Class: UserLogic & Method Name:GetUserById"));
            UserLogic target = new UserLogic(userData, hashData, logData);
            //Act:
            target.GetUserById(id);
            //Assert:
            userData.VerifyAllExpectations();
            logData.VerifyAllExpectations();
        }
        [TestMethod]
        public void GetUserByUsernameAndPasswordTest()
        {
            //Arrange:
            User user = new User();
            string username = "Malcolm";
            string password = "Meow1";
            userData.Expect(mock => mock.CompareLogin(username, "Totally a hashed password")).Return(MockUsers2[0]);
            logData.Expect(mock => mock.LogError("Event", "The user was able to get the user by their user name and password", "Class: UserLogic & Method Name:GetUserByUserNameAndPassword"));
            hashData.Expect(mock => mock.GetHash(password)).Return("Totally a hashed password");
            UserLogic target = new UserLogic(userData, hashData, logData);
            //Act:
            user = target.GetUserByUsernameAndPassword(username, password);
            //Assert:
            logData.VerifyAllExpectations();
            userData.VerifyAllExpectations();
            hashData.VerifyAllExpectations();
            Assert.AreEqual(user.Username, username);
        }
        [TestMethod]
        public void EditUSerTest()
        {
            //Arrange
            User person = new User();
            person.Username = "Ted";
            string password = "Ebear";
            string confPass = "Ebear";
            person.Id = 0;
            hashData.Expect(mock => mock.GetHash(password)).Return("Totally a hashed password");
            UserLogic target = new UserLogic(userData, hashData, logData);
            //Act
            string message = target.EditUser(person, password, confPass);
            //Assert
            userData.VerifyAllExpectations();
            hashData.VerifyAllExpectations();
            Assert.AreEqual("You have successfully updated the user!", message);
        }
        [TestMethod]
        public void CreateUserRedirectTest()
        {
            //Arrange
            User person = new User();
            person.Role = "Admin";
            person.Password = "Yoddle";
            string confPass = "Yo";
            UserLogic target = new UserLogic(userData, hashData, logData);
            //Act
            string result = target.CreateUserRedirect(person, person.Role, confPass);
            //Assert
            userData.VerifyAllExpectations();
            Assert.AreEqual("Your passwords do not match", result);
        }
        [TestMethod]
        public void CreateUserReturnLocationTest()
        {
            //Arrange
            User user = new User();
            string loc = "Index";
            UserLogic target = new UserLogic(userData, hashData, logData);
            //Act
            loc = target.CreateUserReturnLocation(user, loc);
            //Assert
            Assert.AreEqual("Index", loc);
        }
    }
}
