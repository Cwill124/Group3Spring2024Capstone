using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCapstone.model;

namespace DesktopTest.ModelTests
{
    [TestClass]
    public class LoginInfoTest
    {
        [TestMethod]
        public void TestDefaultLoginInfo()
        {
            LoginInfo loginInfo = new LoginInfo();
            Assert.AreEqual("", loginInfo.Username);
            Assert.AreEqual("", loginInfo.Password);
        }

        [TestMethod]
        public void TestParameterizedLoginInfo()
        {
            LoginInfo loginInfo = new LoginInfo("username", "password");
            Assert.AreEqual("username", loginInfo.Username);
            Assert.AreEqual("password", loginInfo.Password);
        }

    }
}
