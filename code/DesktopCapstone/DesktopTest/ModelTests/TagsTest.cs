using DesktopCapstone.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopTest.ModelTests
{
    [TestClass]
    public class TagsTest
    {
        [TestMethod]
        public void ToString_ReturnsTagValue()
        {
            // Arrange
            Tags tags = new Tags
            {
                TagId = 1,
                Tag = "TestTag",
                Note = 123
            };

            // Act
            string result = tags.ToString();

            // Assert
            Assert.AreEqual("TestTag", result);
        }
        [TestMethod]
        public void CreateTag_CreatesTagSuccessfully()
        {
            // Arrange
            int expectedTagId = 1;
            string expectedTagName = "TestTag";
            int expectedNote = 123;

            // Act
            Tags tag = new Tags
            {
                TagId = expectedTagId,
                Tag = expectedTagName,
                Note = expectedNote
            };

            // Assert
            Assert.IsNotNull(tag);
            Assert.AreEqual(expectedTagId, tag.TagId);
            Assert.AreEqual(expectedTagName, tag.Tag);
            Assert.AreEqual(expectedNote, tag.Note);
        }
    }
}
