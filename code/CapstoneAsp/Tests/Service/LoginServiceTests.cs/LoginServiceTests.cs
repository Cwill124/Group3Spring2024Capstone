using System.Diagnostics.CodeAnalysis;
using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Service.LoginServiceTests.cs;

[ExcludeFromCodeCoverage]
[TestFixture]
public class LoginServiceTests
{
    #region Data members

    private ILoginRepository repository;

    private IUserRepository userRepository;

    #endregion

    [SetUp]
    public void Setup()
    {
        var context = new MockDataContext();

        this.repository = new Database.Repository.LoginRepository(context);

        this.userRepository = new UserRepository(context);
    }

    [Test]
    public void NotNullTests()
    {
        var loginRepo = new LoginService(this.repository,this.userRepository);


        Assert.IsNotNull(loginRepo);
    }

    [Test]
    public async Task TestGetUserPassword()
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

        Assert.AreEqual(newUserLogin.Username,found.Username);
    }
}