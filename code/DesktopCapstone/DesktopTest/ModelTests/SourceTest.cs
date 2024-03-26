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
        public void TestSourceCreation()
        {
            Source source = new Source
            {
                SourceId = 1,
                Description = "Test Description",
                Name = "Test Source",
                Content = "Test Content",
                MetaData = "Test Data",
                SourceType = 1,
                Tags = "Test Tags",
                CreatedBy = "Test User"
            };
            Assert.AreEqual(1, source.SourceId);
            Assert.AreEqual(1, source.SourceType);
            Assert.AreEqual("Test Description", source.Description);
            Assert.AreEqual("Test Source", source.Name);
            Assert.AreEqual("Test Content", source.Content);
            Assert.AreEqual("Test Data", source.MetaData);
            Assert.AreEqual("Test Tags", source.Tags);
            Assert.AreEqual("Test User", source.CreatedBy);
            
        }

        [TestMethod]
        public void TestToString()
        {
            Source source = new Source
            {
                SourceId = 1,
                Description = "Test Description",
                Name = "Test Source",
                Content = "Test Content",
                MetaData = "Test Data",
                SourceType = 1,
                Tags = "Test Tags",
                CreatedBy = "Test User"
            };
            Assert.AreEqual("Test Source Description: Test Description", source.ToString());
        }


    }
}
