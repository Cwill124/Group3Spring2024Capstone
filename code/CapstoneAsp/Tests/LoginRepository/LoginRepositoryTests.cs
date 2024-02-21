using CapstoneASP.Database.DBContext;
using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.LoginRepository
{
    public class LoginRepositoryTests
    {
        #region Data members

        private IDataContext context;

        private ILoginRepository repository;

        #endregion

        #region Methods

        [SetUp]
        public void Setup()
        {
            this.context = new MockDataContext();
            this.repository = new Database.Repository.LoginRepository(this.context);
        }

        [Test]
        public void NotNullTest()
        {
            var loginRepo = new Database.Repository.LoginRepository(this.context);

            Assert.IsNotNull(loginRepo);
        }

        [Test]
        public async Task TestGetUserLogin()
        {
            var testUserLogin = new UserLogin
            {
                Username = "User 1",
                Password = "placeHolder"
            };

            var userLogin = await this.repository.GetUserLogin(testUserLogin);

            var expected = MockDataContext.UserLogins.Where(x => x.Username.Equals(testUserLogin.Username)).ElementAt(0);


            Assert.AreEqual(userLogin.Username, expected.Username);

        }
        [Test]
        public async Task CreateUserLogin()
        {
            var newUserLogin = new UserLogin()
            {
                Password = "new",
                Username = "New User"
            };
            await this.repository.CreateAccount(newUserLogin);

            var found = MockDataContext.UserLogins.Where(x => x.Username.Equals(newUserLogin.Username)).ElementAt(0);

            Assert.AreEqual(newUserLogin.Username, found.Username);
        }
        #endregion
    }
}
