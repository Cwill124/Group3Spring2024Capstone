using CapstoneASP.Controllers;
using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Controllers.LoginControllerTests;

public class LoginControllerTests
{
    #region Methods

    [SetUp]
    public void SetUp()
    {
        var context = new MockDataContext();
        this.loginRepository = new LoginRepository(context);
        this.userRepository = new UserRepository(context);
        this.loginService = new LoginService(this.loginRepository, this.userRepository);
        this.loginController = new LoginController(null, this.loginService);
    }

    [Test]
    public void TestNotNull()
    {
        var loginController = new LoginController(null, this.loginService);

        Assert.IsNotNull(loginController);
    }

    [Test]
    public void TestConfigBeingNull()
    {
        //will need to replace this later once I figure out how to mock the IConfiguration
        var userLogin = new UserLogin
        {
            UserId = 2,
            Username = "User 2",
            Password = "Password 2"
        };
        Assert.ThrowsException<AggregateException>(() => this.loginController.Login(userLogin).Result);
    }

    [Test]
    public void RegisterVaild()
    {
        var userLogin = new User
        {
            Password = "user%",
            Username = "User 5",
            Email = "",
            Firstname = "fake",
            Lastname = "eppy",
            Phone = "1231231231"
        };

        Assert.IsInstanceOfType<OkResult>(this.loginController.Register(userLogin).Result);
    }

    [Test]
    public void RegisterInVaild()
    {
        Assert.IsInstanceOfType<BadRequestObjectResult>(this.loginController.Register(null).Result);
    }

    #endregion

    #region data members

    private ILoginService loginService;

    private ILoginRepository loginRepository;

    private IUserRepository userRepository;

    private LoginController loginController;

    #endregion
}