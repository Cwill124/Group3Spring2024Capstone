using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneASP.Model;
using System.Diagnostics.CodeAnalysis;

namespace CapstoneASP.Tests.Model.ProjectAndSourcesTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ProjectAndSourcesTests
    {
        #region Methods

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void Equals_SameProjectIdAndSources_ReturnsTrue()
        {
            // Arrange
            var project1 = new ProjectAndSources { projectId = 1, sources = new List<int> { 1, 2, 3 } };
            var project2 = new ProjectAndSources { projectId = 1, sources = new List<int> { 1, 2, 3 } };

           Assert.AreEqual(project1.projectId,project2.projectId);
           Assert.AreEqual(project1.sources.ElementAt(0), project2.sources.ElementAt(0));
           Assert.AreEqual(project1.sources.ElementAt(1), project2.sources.ElementAt(1));
           Assert.AreEqual(project1.sources.ElementAt(2), project2.sources.ElementAt(2));
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void Equals_DifferentProjectId_ReturnsFalse()
        {
            // Arrange
            var project1 = new ProjectAndSources { projectId = 1, sources = new List<int> { 1, 2, 3 } };
            var project2 = new ProjectAndSources { projectId = 2, sources = new List<int> { 1, 2, 3 } };

            // Act
            var result = project1.Equals(project2);

            // Assert
            Assert.IsFalse(result);
        }


        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void Equals_CompareWithNull_ReturnsFalse()
        {
            // Arrange
            var project = new ProjectAndSources { projectId = 1, sources = new List<int> { 1, 2, 3 } };

            // Act
            var result = project.Equals(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void Equals_CompareWithDifferentType_ReturnsFalse()
        {
            // Arrange
            var project = new ProjectAndSources { projectId = 1, sources = new List<int> { 1, 2, 3 } };

            // Act
            var result = project.Equals("not a ProjectAndSources");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void TestCreatingValidProjectAndSources()
        {
            var project = new ProjectAndSources
            {
                projectId = 1,
                sources = new List<int> { 1, 2, 3 }
            };

            Assert.AreEqual(1, project.projectId);
            CollectionAssert.AreEqual(new List<int> { 1, 2, 3 }, project.sources);
        }

        #endregion
    }
}
