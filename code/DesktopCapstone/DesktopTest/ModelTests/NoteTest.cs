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
        public void TestMethod1()
        {
            Note note = new Note();
            Assert.AreEqual("", note.Content);
            Assert.AreEqual("", note.Username);
            Assert.AreEqual(0, note.SourceId);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Note note = new Note(1, "content", "username");
            Assert.AreEqual("content", note.Content);
            Assert.AreEqual("username", note.Username);
            Assert.AreEqual(1, note.SourceId);
        }

        

    }
}
