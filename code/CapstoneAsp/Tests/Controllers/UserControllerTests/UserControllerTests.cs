using CapstoneASP.Controllers;
using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Controllers.UserControllerTests
{
    public class UserControllerTests
    {
        #region data members

        private IUserService userService;

        private IUserRepository userRepository;

        private UserController UserController;


        #endregion


        [SetUp]
        public void SetUp()
        {
            var context = new MockDataContext();

            this.userRepository = new UserRepository(context);
            this.userService = new UserService(this.userRepository);
            this.UserController = new UserController(null, this.userService);
        }

        [Test]
        public void TestNotNull()
        {
            var userController = new UserController(null, this.userService);
            Assert.IsNotNull(userController);
        }

        [Test]
        public void GetUserByUsernameValid()
        {
            var user = new User()
            {
                Username = "User 1",
                Email = "test@gmail.com",
                Firstname = "Fname 1",
                Lastname = "Lname 2",
                Password = "",
                Phone = "6789081232"
            };
            Assert.IsInstanceOfType<OkObjectResult>(this.UserController.GetUserByUsername(user).Result);
        }
        [Test]
        public void GetUserByUsernameNullInValid()
        {
            
            Assert.IsInstanceOfType<BadRequestObjectResult>(this.UserController.GetUserByUsername(null).Result);
        }
    }
}
