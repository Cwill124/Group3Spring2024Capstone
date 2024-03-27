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
using DesktopCapstone.viewmodel;
using Moq;
using Moq.Dapper;

namespace DesktopTest.ViewModelTests
{
    [TestClass]
    public class SingleProjectViewerModelTest
    {
        [TestMethod]
        public void TestLoadProjectSources()
        {
            // Arrange
            var project = new Project
            {
                ProjectId = 1,
                Title = "testProjectName",
                Description = "testProjectDescription",
                Owner = "test user"
            };

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<Source>(SqlConstants.GetSourcesByProjectId, new { projectId = 1 }, null, true, null, null))
                .Returns(new List<Source>
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

            var dal = new ProjectDAL(mockConnection.Object);
            var viewModel = new SingleProjectViewerModel(project, dal, "test");

            // Act
            viewModel.LoadProjectSources();

            // Assert
            Assert.AreEqual(1, viewModel.ProjectSources.Count);

        }

        [TestMethod]
        public void TestLoadUserSources()
        {
            var project = new Project
            {
                ProjectId = 1,
                Title = "testProjectName",
                Description = "testProjectDescription",
                Owner = "test user"
            };

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<Source>(SqlConstants.GetSourcesNotInProject, new { projectId = 1 }, null, true, null, null))
                .Returns(new List<Source>
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

            var dal = new ProjectDAL(mockConnection.Object);
            var viewModel = new SingleProjectViewerModel(project, dal, "test");

            // Act
            viewModel.LoadUsersSources();

            // Assert
            Assert.AreEqual(1, viewModel.UsersSources.Count);
        }

        [TestMethod]
        public void TestRemoveSourcesFromProject()
        {
            var project = new Project
            {
                ProjectId = 1,
                Title = "testProjectName",
                Description = "testProjectDescription",
                Owner = "test user"
            };

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<Source>(SqlConstants.GetSourcesNotInProject, new { projectId = 1 }, null, true, null, null))
                .Returns(new List<Source>
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
            var dal = new ProjectDAL(mockConnection.Object);
            var viewModel = new SingleProjectViewerModel(project, dal, "test");
            var sourcesToRemove = new List<Source>
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
            };

            viewModel.RemoveSourcesFromProject(sourcesToRemove);
            Assert.AreEqual(2, viewModel.UsersSources.Count);

        }

        [TestMethod]
        public void TestCreateProjectSourcesExport()
        {
            var project = new Project
            {
                ProjectId = 1,
                Title = "testProjectName",
                Description = "testProjectDescription",
                Owner = "test user"
            };

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<Source>(SqlConstants.GetSourcesNotInProject, new { projectId = 1 }, null, true, null, null))
                .Returns(new List<Source>
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
            var dal = new ProjectDAL(mockConnection.Object);
            var viewModel = new SingleProjectViewerModel(project, dal, "test");
            var sourcesToExport = new ObservableCollection<Source>()
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
            };
            viewModel.ProjectSources = sourcesToExport;
            var exportString = " ";
            exportString = viewModel.CreateProjectSourcesExport();
            Assert.AreNotEqual(" ", exportString);

        }
    }
}
