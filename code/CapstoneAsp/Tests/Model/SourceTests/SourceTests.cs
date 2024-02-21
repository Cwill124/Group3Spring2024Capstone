using System.Diagnostics.CodeAnalysis;
using CapstoneASP.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneASP.Tests.Model.SourceTests;

[TestClass]
[ExcludeFromCodeCoverage]
public class SourceTests
{
    #region Methods

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void Equals_SameSourceId_ReturnsTrue()
    {
        // Arrange
        var source1 = new Source { Source_Id = 1 };
        var source2 = new Source { Source_Id = 1 };

        // Act
        var result = source1.Equals(source2);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void Equals_DifferentSourceId_ReturnsFalse()
    {
        // Arrange
        var source1 = new Source { Source_Id = 1 };
        var source2 = new Source { Source_Id = 2 };

        // Act
        var result = source1.Equals(source2);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void GetHashCode_ReturnsHashCode()
    {
        // Arrange
        var source = new Source { Source_Id = 1 };

        // Act
        var hashCode = source.GetHashCode();

        // Assert
        Assert.AreEqual(source.Source_Id.GetHashCode(), hashCode);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void Equals_CompareWithNull_ReturnsFalse()
    {
        // Arrange
        var source = new Source { Source_Id = 1 };

        // Act
        var result = source.Equals(null);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void Equals_CompareWithDifferentType_ReturnsFalse()
    {
        // Arrange
        var source = new Source { Source_Id = 1 };

        // Act
        var result = source.Equals("not a Source");

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void TestCreatingValidSource()
    {
        var source = new Source
        {
            Source_Id = 1,
            Name = "Test Source",
            Description = "Test Description",
            Content = "Test Content",
            Meta_Data = "Test Meta_Data",
            Tags = "Test Tags",
            Source_Type_Id = 1,
            Created_By = "Test User"
        };

        Assert.AreEqual(1, source.Source_Id);
        Assert.AreEqual("Test Source", source.Name);
        Assert.AreEqual("Test Description", source.Description);
        Assert.AreEqual("Test Content", source.Content);
        Assert.AreEqual("Test Meta_Data", source.Meta_Data);
        Assert.AreEqual("Test Tags", source.Tags);
        Assert.AreEqual(1, source.Source_Type_Id);
        Assert.AreEqual("Test User", source.Created_By);
    }

    #endregion
}