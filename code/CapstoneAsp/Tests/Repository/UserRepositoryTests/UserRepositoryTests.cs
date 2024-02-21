using CapstoneASP.Database.DBContext;
using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Repository.UserRepositoryTests
{
    public class UserRepositoryTests
    {
        #region Data members

        private IDataContext context;

        private IUserRepository userRepository;

        #endregion

        #region Methods

        [SetUp]
        public void Setup()
        {
            this.context = new MockDataContext();
            this.userRepository = new UserRepository(this.context);
        }

        [Test]
        public void NotNullTest()
        {
            var userRepository = new UserRepository(this.context);

            Assert.IsNotNull(userRepository);
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
            var foundUser = await this.userRepository.GetUserByUsername(newUser);

            Assert.AreEqual(newUser.Username, foundUser.Username);
        }

        [Test]
        public async Task CreateUser()
        {
            var newUser = new User
            {
                Username = "new User",
                Email = "new@yahoo.com",
                Firstname = "firstName",
                Lastname = "lastName",
                Password = "",
                Phone = "7702321232"
            };
            await this.userRepository.CreateUser(newUser);

            var found = MockDataContext.Users.Where(x => x.Username.Equals(newUser.Username)).ElementAt(0);


            Assert.AreEqual(newUser.Username,found.Username);
        }

        #endregion
    }
}
