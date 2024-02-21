using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using DesktopCapstone.util;
using Moq;
using Moq.Dapper;

namespace DesktopTest.DALTests
{
    [TestClass]
    public class NoteDALTest
    {

        [TestMethod]
        public void TestCreateNote()
        {
            // Arrange
            var title = "testTitle";
            var content = "testContent";
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Execute(It.IsAny<string>(), It.IsAny<object>(), null, null, null)).Returns(1);
            var noteDAL = new NoteDAL(mockConnection.Object);

            // Act
            var note = new Note
            {
                SourceId = 1,
                NoteId = 1,
                Content = content,
                Username = "testUsername"
            };
            var result = noteDAL.CreateNote(note);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestGetNotesById()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<dynamic>(SqlConstants.GetNotesById, It.IsAny<object>(), null, true, null, null))
                .Returns(new List<dynamic>
                {
                    new
                    {   source_id = 1,
                        note_id = 1,
                        content = "testContent",
                        username = "testUsername"

                    }
                });
            var noteDAL = new NoteDAL(mockConnection.Object);
            var notes = noteDAL.GetNoteById(1);
            Assert.AreEqual(1, notes.Count);
        }
        


    }
}
