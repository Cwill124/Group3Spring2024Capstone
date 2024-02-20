using desktop_capstone.DAL;
using DesktopCapstone.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DesktopCapstone.DAL;
using DesktopCapstone.util;
using Moq;

namespace DesktopTest.DALTests
{
    [TestClass]
    public class SourceDALTest
    {
        private Mock<IDbConnection> mockConnection;
        private SourceDAL _sourceDAL;
        //private Mock<IDbConnection> mockDbConnection;

        [TestMethod]
        public void TestGetAllSources()
        {
            var expected = new ObservableCollection<Source>();
            // Arrange
            mockConnection.Setup(x => x.Query<Source>(SqlConstants.GetAllSources,null,null,true,null, null))
                .Returns(expected);
            //SourceDAL sourceDAL = new SourceDAL(mockConnection.Object);

            // Act
            //ObservableCollection<Source> sources = sourceDAL.GetAllSources();

            // Assert
            //Assert.AreEqual(expected.Count, sources.Count);
        }


    }
}
