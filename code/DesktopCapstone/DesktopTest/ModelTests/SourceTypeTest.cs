using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCapstone.model;

namespace DesktopTest.ModelTests
{
    [TestClass]
    public class SourceTypeTest
    {
        [TestMethod]
        public void TestSourceTypeCreation()
        {
            SourceType sourceType = new SourceType
            {
                SourceTypeId = 1,
                TypeName = "Test Source Type"
            };
            Assert.AreEqual(1, sourceType.SourceTypeId);
            Assert.AreEqual("Test Source Type", sourceType.TypeName);
        }

        [TestMethod]
        public void TestSourceTypeToString()
        {
            SourceType sourceType = new SourceType
            {
                SourceTypeId = 1,
                TypeName = "Test Source Type"
            };
            Assert.AreEqual("Test Source Type", sourceType.ToString());
        }
    }
}
