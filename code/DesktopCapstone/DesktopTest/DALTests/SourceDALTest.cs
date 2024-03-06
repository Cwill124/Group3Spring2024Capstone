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
            mockConnection.SetupDapper(x => x.Query<dynamic>(SqlConstants.GetAllSources,null, null, true, null, null))
                .Returns(new List<dynamic>
                {
                    new
                    {   source_id = 1,
                        name = "testName",
                        descriptiom = "testDescription",
                        content = "testContent",
                        meta_data = "testMetaData",
                        source_type_id = 1,
                        tags = "testTags",
                        created_by = "testCreatedBy"

                    }
                });
            var sourceDAL = new SourceDAL(mockConnection.Object);
            var sources = sourceDAL.GetAllSources();
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
                    SourceType = 1,
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
            mockConnection.SetupDapper(x => x.Query<dynamic>(SqlConstants.GetSourceTypes, null, null, true, null, null))
                .Returns(new List<dynamic>
                {
                    new { source_type_id = 1, type_name = "testSourceTypeName" }
                });
            var sourceDAL = new SourceDAL(mockConnection.Object);
            var sourceTypes = sourceDAL.GetSourceTypes();
            Assert.AreEqual(1, sourceTypes.Count);
        }

        [TestMethod]
        public void TestCreateSource()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Execute(SqlConstants.CreateSource, It.IsAny<object>(), null, null, null))
                .Returns(1);
            var sourceDAL = new SourceDAL(mockConnection.Object);
            var source = new Source
            {
                Name = "testSourceName",
                Content = "testSourceContent",
                SourceType = 1,
                MetaData = "testSourceURL",
                Description = "testSourceDescription",
                Tags = "testSourceTags",
                CreatedBy = "testSourceCreatedBy",
            };
            var result = sourceDAL.CreateSource(source);
            
            Assert.IsTrue(result);
        }
        
        

    }
}
