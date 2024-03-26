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
    public class SourcesCreationViewModelTest
    {
        [TestMethod]
        public void TestViewModelCreation()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<SourceType>(SqlConstants.GetSourceTypes, null, null, true, null, null))
                .Returns(new List<SourceType>
                {
                    new SourceType
                    {
                        
                        SourceTypeId = 1,
                        TypeName = "test"
                    }
                });
            var dal = new SourceDAL(mockConnection.Object);
            var viewModel = new SourceCreationViewModel(dal);
            Assert.AreEqual(1, viewModel.SourceTypes.Count);
        }
    }
}
