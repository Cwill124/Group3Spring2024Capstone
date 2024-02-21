using desktop_capstone.DAL;
using desktop_capstone.model;
using DesktopCapstone.model;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DesktopCapstone.util;
using Moq.Dapper;
using Npgsql;

namespace DesktopTest.DALTests
{

    [TestClass]
    public class LoginDALTest
    {

        [TestMethod]
        public void TestCheckLogin()
        {
            // Arrange
            var username = "testUsername";
            var password = "testPassword";
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.QuerySingleOrDefault<AppUser>(SqlConstants.CheckLogin, It.IsAny<object>(), null, null, null))
                .Returns(new AppUser
                {
                    Username = username,
                    FirstName = "testFirstName",
                    LastName = "testLastName",
                    Email = "testEmail",
                    PhoneNumber = "testPhoneNumber",
                });
            var loginDAL = new LoginDAL(mockConnection.Object);

            // Act
            var result = loginDAL.checkLogin(username, password);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestCheckIfUsernameIsInUse()
        {
            var username = "existingUsername";
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => (x.QuerySingleOrDefault<LoginInfo>(SqlConstants.CheckIfUsernameInUse, new { username = username }, null, null, null))).Returns(new LoginInfo
            {
                Username = "existingUsername",
                Password = "existingPassword"
            });

            var loginDAL = new LoginDAL(mockConnection.Object);

            // Act
            var result = loginDAL.checkIfUsernameIsInUse(username);

            // Assert
            Assert.IsTrue(result);
        }
        
    }
}
