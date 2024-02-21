using desktop_capstone.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopTest.ModelTests
{
    [TestClass]
    public class AppUserTest
    {
        [TestMethod]
        public void TestDefaultAppUser()
        {
            AppUser user = new AppUser();
            Assert.AreEqual("", user.Username);
            Assert.AreEqual("", user.Email);
            Assert.AreEqual("", user.FirstName);
            Assert.AreEqual("", user.LastName);
        }

        [TestMethod]
        public void TestParameterizedAppUser()
        {
            AppUser user = new AppUser("username", "firstName", "lastName", "email", "phonenumber");
            Assert.AreEqual("username", user.Username);
            Assert.AreEqual("email", user.Email);
            Assert.AreEqual("firstName", user.FirstName);
            Assert.AreEqual("lastName", user.LastName);
        }

    }
}
