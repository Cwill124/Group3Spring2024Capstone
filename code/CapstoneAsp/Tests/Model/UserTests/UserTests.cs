using CapstoneASP.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneASP.Tests.Model.UserTests;

[TestClass]
public class UserTests
{
    #region Methods

    [TestMethod]
    public void Equals_SameUsername_ReturnsTrue()
    {
        // Arrange
        var user1 = new User { Username = "TestUser" };
        var user2 = new User { Username = "TestUser" };

        // Act
        var result = user1.Equals(user2);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Equals_DifferentUsernameCase_ReturnsTrue()
    {
        // Arrange
        var user1 = new User { Username = "TestUser" };
        var user2 = new User { Username = "testuser" };

        // Act
        var result = user1.Equals(user2);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Equals_DifferentUsername_ReturnsFalse()
    {
        // Arrange
        var user1 = new User { Username = "TestUser1" };
        var user2 = new User { Username = "TestUser2" };

        // Act
        var result = user1.Equals(user2);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GetHashCode_ReturnsHashCode()
    {
        // Arrange
        var user = new User { Username = "TestUser" };

        // Act
        var hashCode = user.GetHashCode();

        // Assert
        Assert.AreEqual(StringComparer.OrdinalIgnoreCase.GetHashCode("TestUser"), hashCode);
    }

    [TestMethod]
    public void Equals_CompareWithNull_ReturnsFalse()
    {
        // Arrange
        var user = new User { Username = "TestUser" };

        // Act
        var result = user.Equals(null);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Equals_CompareWithDifferentType_ReturnsFalse()
    {
        // Arrange
        var user = new User { Username = "TestUser" };

        // Act
        var result = user.Equals("not a User");

        // Assert
        Assert.IsFalse(result);
    }

    #endregion
}