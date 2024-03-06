using DesktopCapstone.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopTest.ModelTests
{
    [TestClass]
    public class NoteTest
    {
        [TestMethod]
        public void TestNoteCreation()
        {
            Note note = new Note
            {
                Content = "Test Content",
                NoteId = 1,
                SourceId = 1,
                Username = "Test User"
            };
            
            Assert.AreEqual(1, note.NoteId);
            Assert.AreEqual(1, note.SourceId);
            Assert.AreEqual("Test Content", note.Content);
            Assert.AreEqual("Test User", note.Username);
        }

        [TestMethod]
        public void TestNoteToString()
        {
            Note note = new Note
            {
                Content = "{\"note_Title\":\"Test Title\",\"note_Content\":\"Test Content\"}",
                NoteId = 1,
                SourceId = 1,
                Username = "Test User"
            };
            var s = note.ToString();

            Assert.AreEqual("Test Title\n\nTest Content", note.ToString());
        }

        [TestMethod]
        public void HasTag_TagExists_ReturnsTrue()
        {
            // Arrange
            Tags tag = new Tags { TagId = 1, Tag = "TestTag" };
            Note note = new Note { NoteId = 1, TagList = new ObservableCollection<Tags> { tag } };

            // Act
            bool result = note.HasTag(tag);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasTag_TagDoesNotExist_ReturnsFalse()
        {
            // Arrange
            Tags tag = new Tags { TagId = 1, Tag = "TestTag" };
            Note note = new Note { NoteId = 1, TagList = new ObservableCollection<Tags>() };

            // Act
            bool result = note.HasTag(tag);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetTitle_ReturnsNoteTitle()
        {
            // Arrange
            Note note = new Note { Content = "{\"note_Title\":\"TestTitle\",\"note_Content\":\"TestContent\"}" };

            // Act
            string result = note.GetTitle();

            // Assert
            Assert.AreEqual("TestTitle", result);
        }

        [TestMethod]
        public void GetContent_ReturnsNoteContent()
        {
            // Arrange
            Note note = new Note { Content = "{\"note_Title\":\"TestTitle\",\"note_Content\":\"TestContent\"}" };

            // Act
            string result = note.GetContent();

            // Assert
            Assert.AreEqual("TestContent", result);
        }

    }
}
