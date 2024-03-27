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
using Moq.Dapper;
using Npgsql;

namespace DesktopTest.DALTests
{
    [TestClass]
    public class SourceDALTest
    {

        [TestMethod]
        public void TestGetAllSources()
        {
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

            var sourceDAL = new SourceDAL(mockConnection.Object);
            var sources = sourceDAL.GetAllSourcesByUser("test user");

            Assert.AreEqual(1, sources.Count);
        }


        [TestMethod]
        public void TestGetSourceWithId()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.QuerySingleOrDefault<Source>(SqlConstants.GetSourceById, new { id = 1 }, null, null, null))
                .Returns(new Source
                {
                    SourceId = 1,
                    Name = "testSourceName",
                    Content = "testSourceContent",
                    SourceTypeId = 1,
                    MetaData = "testSourceURL",
                    Description = "testSourceDescription",
                    Tags = "testSourceTags",
                    CreatedBy = "testSourceCreatedBy",
                });
            var sourceDAL = new SourceDAL(mockConnection.Object);
            var source = sourceDAL.GetSourceWithId(1);
            Assert.AreEqual(1, source.SourceId);
        }

        [TestMethod]
        public void TestGetSourceTypes()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<SourceType>(SqlConstants.GetSourceTypes, null, null, true, null, null))
                .Returns(new List<SourceType>
                {
                    new SourceType() {SourceTypeId = 1, TypeName = "PDF"}
                });
            var sourceDAL = new SourceDAL(mockConnection.Object);
            var sourceTypes = sourceDAL.GetSourceTypes();
            Assert.AreEqual(1, sourceTypes.Count);
        }

        [TestMethod]
        public void TestCreateSource()
        {
            // Arrange
            var mockConnection = new Mock<IDbConnection>();
            var sourceDAL = new SourceDAL(mockConnection.Object);

            mockConnection.SetupDapper(x => x.Execute(SqlConstants.CreateSource, It.IsAny<object>(), null, null, null))
                .Returns(1);

            var source = new Source
            {
                Name = "testSourceName",
                Content = "testSourceContent",
                SourceTypeId = 1,
                MetaData = "testSourceURL",
                Description = "testSourceDescription",
                Tags = "testSourceTags",
                CreatedBy = "testSourceCreatedBy",
            };

            // Act
            var result = sourceDAL.CreateSource(source);

            // Assert
            Assert.AreEqual(1, result); // Assuming 1 represents success
        }



    }
}
