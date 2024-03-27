using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using DesktopCapstone.util;
using Moq;
using Moq.Dapper;

namespace DesktopTest.DALTests
{
    [TestClass]
    public class ProjectDALTest
    {
        [TestMethod]
        public void TestGetProjectsForUser()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<Project>(SqlConstants.GetProjectsForUser, It.IsAny<object>(), null, true, null, null))
                .Returns(new ObservableCollection<Project>()
                {
                    new Project
                    {
                        Description = "test",
                        Owner = "test",
                        ProjectId = 1,
                        Title = "test"
                    }
                });
            var projectDAL = new ProjectDAL(mockConnection.Object);
            var projects = projectDAL.GetProjectsForUser("test");
            Assert.AreEqual(1, projects.Count);
        }

        [TestMethod]
        public void TestGetSourcesFromProject()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<Source>(SqlConstants.GetSourcesByProjectId, It.IsAny<object>(), null, true, null, null))
                .Returns(new ObservableCollection<Source>()
                {
                    new Source
                    {
                        SourceId = 1,
                        Description = "test",
                        Name = "test",
                        Content = "test",
                        MetaData = "test",
                        SourceTypeId = 1,
                        Tags = null,
                        CreatedBy = "test user"
                    }
                });
            var projectDAL = new ProjectDAL(mockConnection.Object);
            var sources = projectDAL.GetSourcesFromProject(1);
            Assert.AreEqual(1, sources.Count);

        }

        [TestMethod]
        public void TestGetSourcesNotInProject()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<Source>(SqlConstants.GetSourcesNotInProject, It.IsAny<object>(), null, true, null, null))
                .Returns(new ObservableCollection<Source>()
                {
                    new Source
                    {
                        SourceId = 1,
                        Description = "test",
                        Name = "test",
                        Content = "test",
                        MetaData = "test",
                        SourceTypeId = 1,
                        Tags = null,
                        CreatedBy = "test user"
                    }
                });
            var projectDAL = new ProjectDAL(mockConnection.Object);
            var sources = projectDAL.GetSourcesNotInProject(1, "test");
            Assert.AreEqual(1, sources.Count);
        }

        [TestMethod]
        public void TestCreateProject()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Execute(SqlConstants.CreateProject, It.IsAny<object>(), null, null, null)).Returns(1);
            var projectDAL = new ProjectDAL(mockConnection.Object);
            var result = projectDAL.AddProject(new Project());
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestAddSourceToProject()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Execute(SqlConstants.AddSourceToProject, new {projectId = 1, sourceId = 1}, null, null, null)).Returns(1);
            
            var projectDAL = new ProjectDAL(mockConnection.Object);
            var result = projectDAL.AddSourceToProject(1, 1);
            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void RemoveSourceFromProject()
        {

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Execute(SqlConstants.RemoveSourceFromProject, It.IsAny<object>(), null, null, null))
                .Returns(1);
            var projectDAL = new ProjectDAL(mockConnection.Object);
            var result = projectDAL.RemoveSourceFromProject(1, 1);
            Assert.AreEqual(1, result);
        }
    }
}
