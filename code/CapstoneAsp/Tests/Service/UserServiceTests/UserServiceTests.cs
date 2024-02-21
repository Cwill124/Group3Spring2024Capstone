using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Service.UserServiceTests
{
    public class UserServiceTests
    {
        #region Data members

        private IUserRepository repository;

        private IUserService userService;

        #endregion

        #region Methods

        [SetUp]
        public void Setup()
        {
            var context = new MockDataContext();

            this.repository = new UserRepository(context);
            this.userService = new UserService(this.repository);
        }

        [Test]
        public void NotNullTest()
        {
            var userRepo = new UserService(this.repository);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(userRepo);
        }

        [Test]
        public async Task GetUserByUsername()
        {
            var newUser = new User
            {
                Email = "",
                Firstname = "",
                Lastname = "",
                Password = "",
                Phone = "",
                Username = "User 1"
            };
            var foundUser = await this.userService.GetUserByUsername(newUser);

            Assert.AreEqual(newUser.Username,foundUser.Username);
        }
        #endregion
    }
}
