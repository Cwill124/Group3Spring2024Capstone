using CapstoneASP.Model;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Model.TagsTests
{
        [TestFixture]
        public class TagsTests
        {
            [Test]
            public void Equals_SameTagId_ReturnsTrue()
            {
                // Arrange
                var tag1 = new Tags { TagId = 1 };
                var tag2 = new Tags { TagId = 1 };

                // Act
                var result = tag1.Equals(tag2);

                // Assert
                Assert.IsTrue(result);
            }

            [Test]
            public void Equals_DifferentTagId_ReturnsFalse()
            {
                // Arrange
                var tag1 = new Tags { TagId = 1 };
                var tag2 = new Tags { TagId = 2 };

                // Act
                var result = tag1.Equals(tag2);

                // Assert
                Assert.IsFalse(result);
            }

            [Test]
            public void GetHashCode_ReturnsHashCodeOfTagId()
            {
                // Arrange
                var tag = new Tags { TagId = 123 };

                // Act
                var hashCode = tag.GetHashCode();

                // Assert
                Assert.AreEqual(123.GetHashCode(), hashCode);
            }
        }
}
