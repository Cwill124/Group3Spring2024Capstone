using System.Diagnostics.CodeAnalysis;
using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using CapstoneASP.Util;
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

    private ILoginService loginService;

    #endregion

    #region Methods

    [SetUp]
    public void Setup()
    {
        var context = new MockDataContext();

        this.repository = new LoginRepository(context);

        this.userRepository = new UserRepository(context);

        this.loginService = new LoginService(this.repository, this.userRepository);
    }

    [Test]
    public void NotNullTests()
    {
        var loginRepo = new LoginService(this.repository, this.userRepository);

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
        var test = PasswordHasher.HashPassword(testUserLogin.Password);
        var userLogin = await this.loginService.GetUserLogin(testUserLogin);

        var expected = MockDataContext.UserLogins.Where(x => x.Username.Equals(testUserLogin.Username)).ElementAt(0);

        Assert.AreEqual(userLogin.Username, expected.Username);
    }

    [Test]
    public async Task TestGetUserPasswordThrowError()
    {
        var testUserLogin = new UserLogin
        {
            Username = "User 1",
            Password = ""
        };
        var userLogin = await this.loginService.GetUserLogin(testUserLogin);

        Assert.IsNull(userLogin);
    }

    [Test]
    public async Task CreateUserLogin()
    {
        var newUserLogin = new UserLogin
        {
            Password = "new",
            Username = "New User"
        };
        var newUser = new User
        {
            Username = newUserLogin.Username,
            Email = "new@yahoo.com",
            Firstname = "firstName",
            Lastname = "lastName",
            Password = "",
            Phone = "7702321232"
        };
        await this.loginService.CreateAccount(newUser);

        var found = MockDataContext.Users.Where(x => x.Username.Equals(newUserLogin.Username)).ElementAt(0);

        Assert.AreEqual(newUserLogin.Username, found.Username);
    }

    #endregion
}