using System.Diagnostics.CodeAnalysis;
using CapstoneASP.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneASP.Tests.Model.UserLoginTests;

[TestClass]
[ExcludeFromCodeCoverage]
public class UserLoginTests
{
    #region Methods

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void Equals_SameUserIdAndUsername_ReturnsTrue()
    {
        // Arrange
        var userLogin1 = new UserLogin { UserId = 1, Username = "TestUser" };
        var userLogin2 = new UserLogin { UserId = 1, Username = "TestUser" };

        // Act
        var result = userLogin1.Equals(userLogin2);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void Equals_DifferentUserId_ReturnsFalse()
    {
        // Arrange
        var userLogin1 = new UserLogin { UserId = 1, Username = "TestUser" };
        var userLogin2 = new UserLogin { UserId = 2, Username = "TestUser" };

        // Act
        var result = userLogin1.Equals(userLogin2);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void Equals_DifferentUsernameCase_ReturnsTrue()
    {
        // Arrange
        var userLogin1 = new UserLogin { UserId = 1, Username = "TestUser" };
        var userLogin2 = new UserLogin { UserId = 1, Username = "testuser" };

        // Act
        var result = userLogin1.Equals(userLogin2);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void GetHashCode_ReturnsHashCode()
    {
        // Arrange
        var userLogin = new UserLogin { UserId = 1, Username = "TestUser" };

        // Act
        var hashCode = userLogin.GetHashCode();

        // Assert
        Assert.AreEqual(userLogin.GetHashCode(), hashCode);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void Equals_CompareWithNull_ReturnsFalse()
    {
        // Arrange
        var userLogin = new UserLogin { UserId = 1, Username = "TestUser" };

        // Act
        var result = userLogin.Equals(null);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void Equals_CompareWithDifferentType_ReturnsFalse()
    {
        // Arrange
        var userLogin = new UserLogin { UserId = 1, Username = "TestUser" };

        // Act
        var result = userLogin.Equals("not a UserLogin");

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void TestCreatingValidUserLogin()
    {
        var userLogin = new UserLogin
        {
            UserId = 1,
            Username = "TestUser",
            Password = "TestPassword"
        };

        Assert.AreEqual(1, userLogin.UserId);
        Assert.AreEqual("TestUser", userLogin.Username);
        Assert.AreEqual("TestPassword", userLogin.Password);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void TestCreatingValidUser()
    {
        var user = new User
        {
            Username = "TestUser",
            Email = "testuser@example.com",
            Firstname = "Test",
            Lastname = "User",
            Phone = "123456789"
        };

        Assert.AreEqual("TestUser", user.Username);
        Assert.AreEqual("testuser@example.com", user.Email);
        Assert.AreEqual("Test", user.Firstname);
        Assert.AreEqual("User", user.Lastname);
        Assert.AreEqual("123456789", user.Phone);
    }

    #endregion
}