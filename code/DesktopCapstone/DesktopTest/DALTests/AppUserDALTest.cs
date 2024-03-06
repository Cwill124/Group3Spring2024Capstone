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
    public class AppUserDALTest
    {

        [TestMethod]
        public void TestCreateNewUser()
        {
            // Arrange
            var username = "testUsername";
            var password = "testPassword";
            var firstName = "testN";
            var lastName = "testL";
            var email = "testEmail";
            var phoneNumber = "testPhone";
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Execute(It.IsAny<string>(), It.IsAny<object>(), null, null, null)).Returns(1);
            var appUserDAL = new AppUserDAL(mockConnection.Object);

            // Act
            var result = appUserDAL.CreateNewUser(username, password, firstName, lastName, email, phoneNumber);

            // Assert
            Assert.IsTrue(result);
        }
        
    }
}
