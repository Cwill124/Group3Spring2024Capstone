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
using Moq;
using Moq.Dapper;

namespace DesktopTest.DALTests
{
    [TestClass]
    public class TagDALTest
    {
        [TestMethod]
        public void TestGetTagsBelongingToUser()
        {
            // Arrange
            var mockConnection = new Mock<IDbConnection>();

            mockConnection.SetupDapper(x => x.Query<Tags>(SqlConstants.GetTagsBelongingToUser, It.IsAny<object>(), null, true, null, null))
                .Returns(new List<Tags>
                {
                    new Tags
                    {
                        TagId = 1,
                        Note = 1,
                        Tag = "tst"
                    }
                });

            var tagDAL = new TagDAL(mockConnection.Object);

            // Act
            var result = tagDAL.GetTagsBelongingToUser("testUser");

            // Assert
            Assert.AreEqual(1, result.Count);
        }
        [TestMethod]
        public void TestCreateTag()
        {
            // Arrange
            var mockConnection = new Mock<IDbConnection>();

            mockConnection.SetupDapper(x => x.Execute(SqlConstants.CreateTag, It.IsAny<object>(), null, null, null))
                .Returns(1); // Assuming 1 represents success

            var tagDAL = new TagDAL(mockConnection.Object);

            var tag = new Tags
            {
                TagId = 1,
                Note = 1,
                Tag = "tst"
            };

            // Act
            var result = tagDAL.CreateTag(tag);

            // Assert
            Assert.AreEqual(1, result); // Assuming 1 represents success
        }

        [TestMethod]
        public void TestDeleteTag()
        {
            // Arrange
            var mockConnection = new Mock<IDbConnection>();

            mockConnection.SetupDapper(x => x.Execute(SqlConstants.DeleteTag, It.IsAny<object>(), null, null, null))
                .Returns(1); // Assuming 1 represents success

            var tagDAL = new TagDAL(mockConnection.Object);

            var tag = new Tags
            {
                TagId = 1,
                Note = 1,
                Tag = "tst"
            };

            // Act
            var result = tagDAL.DeleteTag(tag);

            Assert.AreEqual(1, result); // Assuming 1 represents success
        }
    }
}
