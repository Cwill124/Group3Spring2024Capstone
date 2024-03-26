using System;
using System.Collections.Generic;
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
    public class ProjectsViewerViewModelTest
    {
        [TestMethod]
        public void TestMethodRefreshProjects()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<Project>(SqlConstants.GetAllProjects, null, null, true, null, null))
                .Returns(new List<Project>
                {
                    new Project
                    {
                        ProjectId = 1,
                        Title = "test",
                        Description = "test",
                        Owner = "test user"
                    }
                });
            var dal = new ProjectDAL(mockConnection.Object);
            var viewModel = new ProjectsViewerViewModel("test user", dal);
            Assert.AreEqual(1, viewModel.Projects.Count);

        }
    }
}
