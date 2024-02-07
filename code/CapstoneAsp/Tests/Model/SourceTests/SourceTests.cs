using System.Diagnostics.CodeAnalysis;
using CapstoneASP.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneASP.Tests.Model.SourceTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SourceTests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void Equals_SameSourceId_ReturnsTrue()
        {
            // Arrange
            var source1 = new Source { SourceId = 1 };
            var source2 = new Source { SourceId = 1 };

            // Act
            bool result = source1.Equals(source2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void Equals_DifferentSourceId_ReturnsFalse()
        {
            // Arrange
            var source1 = new Source { SourceId = 1 };
            var source2 = new Source { SourceId = 2 };

            // Act
            bool result = source1.Equals(source2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void GetHashCode_ReturnsHashCode()
        {
            // Arrange
            var source = new Source { SourceId = 1 };

            // Act
            int hashCode = source.GetHashCode();

            // Assert
            Assert.AreEqual(source.SourceId.GetHashCode(), hashCode);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void Equals_CompareWithNull_ReturnsFalse()
        {
            // Arrange
            var source = new Source { SourceId = 1 };

            // Act
            bool result = source.Equals(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void Equals_CompareWithDifferentType_ReturnsFalse()
        {
            // Arrange
            var source = new Source { SourceId = 1 };

            // Act
            bool result = source.Equals("not a Source");

            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void TestCreatingValidSource()
        {
            var source = new Source()
            {
                SourceId = 1,
                Name = "Test Source",
                Description = "Test Description",
                Content = "Test Content",
                MetaData = "Test MetaData",
                Tags = "Test Tags",
                SourceTypeId = 1,
                CreatedBy = "Test User"
            };

            Assert.AreEqual(1, source.SourceId);
            Assert.AreEqual("Test Source", source.Name);
            Assert.AreEqual("Test Description", source.Description);
            Assert.AreEqual("Test Content", source.Content);
            Assert.AreEqual("Test MetaData", source.MetaData);
            Assert.AreEqual("Test Tags", source.Tags);
            Assert.AreEqual(1, source.SourceTypeId);
            Assert.AreEqual("Test User", source.CreatedBy);
        }
    }
}

