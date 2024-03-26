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
    public class SourcesViewerViewModelTest
    {
        [TestMethod]
        public void TestRefreshSources()
        {
            // Arrange
            var username = "test user";
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<Source>(SqlConstants.GetAllSources, null, null, true, null, null))
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
            var dal = new SourceDAL(mockConnection.Object);
            var viewModel = new SourcesViewerViewModel(username, dal);

            // Act
            viewModel.RefreshSources();

            // Assert
            Assert.AreEqual(1, viewModel.Sources.Count);

        }
    }
}
