using DesktopCapstone.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopTest.ModelTests
{
    [TestClass]
    public class SourceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Source source = new Source();
            Assert.AreEqual("", source.Description);
            Assert.AreEqual("", source.Name);
            Assert.AreEqual("", source.Content);
            Assert.AreEqual("", source.MetaData);
            Assert.AreEqual(0, source.SourceType);
            Assert.AreEqual("", source.Tags);
            Assert.AreEqual("", source.CreatedBy);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Source source = new Source("description", "name", "content", "metadata", 1, "tags", "createdBy");
            Assert.AreEqual("description", source.Description);
            Assert.AreEqual("name", source.Name);
            Assert.AreEqual("content", source.Content);
            Assert.AreEqual("metadata", source.MetaData);
            Assert.AreEqual(1, source.SourceType);
            Assert.AreEqual("tags", source.Tags);
            Assert.AreEqual("createdBy", source.CreatedBy);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Source source = new Source("description", "name", "content", "metadata", 1, "tags", "createdBy");
            Assert.AreEqual("name", source.ToString());
        }

    }
}
