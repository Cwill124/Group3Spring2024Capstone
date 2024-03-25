using CapstoneASP.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneASP.Tests.Model.ProjectTests
{
    [TestClass]
    public class ProjectTests
    {
        [TestMethod]
        public void Equals_SameProjectId_ReturnsTrue()
        {
            // Arrange
            var project1 = new Project { ProjectId = 1 };
            var project2 = new Project { ProjectId = 1 };

            // Act
            var result = project1.Equals(project2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_DifferentProjectId_ReturnsFalse()
        {
            // Arrange
            var project1 = new Project { ProjectId = 1 };
            var project2 = new Project { ProjectId = 2 };

            // Act
            var result = project1.Equals(project2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetHashCode_ReturnsHashCode()
        {
            // Arrange
            var project = new Project { ProjectId = 1 };

            // Act
            var hashCode = project.GetHashCode();

            // Assert
            Assert.AreEqual(project.ProjectId.GetHashCode(), hashCode);
        }

        [TestMethod]
        public void Equals_CompareWithNull_ReturnsFalse()
        {
            // Arrange
            var project = new Project { ProjectId = 1 };

            // Act
            var result = project.Equals(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_CompareWithDifferentType_ReturnsFalse()
        {
            // Arrange
            var project = new Project { ProjectId = 1 };

            // Act
            var result = project.Equals("not a Project");

            // Assert
            Assert.IsFalse(result);
        }
    }
}