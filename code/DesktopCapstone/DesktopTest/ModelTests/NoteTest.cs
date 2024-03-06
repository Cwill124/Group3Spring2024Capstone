using DesktopCapstone.model;
using System;
using System.Collections.Generic;
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

        

    }
}
