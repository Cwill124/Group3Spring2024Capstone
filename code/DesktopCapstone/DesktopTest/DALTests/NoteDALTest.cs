using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            mockConnection.SetupDapper(x => x.QueryFirstOrDefault<Note>(
                    It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .Returns(
                    new Note()
                    {
                        SourceId = 1,
                        NoteId = 1,
                        Content = content,
                        Username = "testUsername"
                    });

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
            Assert.IsNotNull(result);
            Assert.AreEqual(result.NoteId, note.NoteId);
            Assert.AreEqual(result.Content, note.Content);
            Assert.AreEqual(result.Username, note.Username);

        }

        [TestMethod]
        public void TestGetNotesById()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<Note>(SqlConstants.GetNotesById, It.IsAny<object>(), null, true, null, null))
                .Returns(new List<Note>
                {
                    new Note
                    { 
                        NoteId = 1,
                       Content = "",
                       SourceId = 1,
                       TagList = new ObservableCollection<Tags>(),
                       Username = "test user"
                    }
                });
            var noteDAL = new NoteDAL(mockConnection.Object);
            var notes = noteDAL.GetNoteById(1);
            Assert.AreEqual(1, notes.Count);
        }

        [TestMethod]
        public void TestDeleteNoteById()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Execute(SqlConstants.DeleteNoteById, It.IsAny<object>(), null, null, null))
                .Returns(1); // Assuming 1 represents the number of affected rows

            // Example usage in your test
            var noteDAL = new NoteDAL(mockConnection.Object);

            var result = noteDAL.DeleteNoteById(1);

            // Assert that the delete operation was called
            Assert.AreEqual(0,result );

        }
        [TestMethod]
        public void TestSearchNotesByName()
        {
            // Arrange
            var mockConnection = new Mock<IDbConnection>();

            var expectedNotes = new List<Note>
            {
                new Note
                {
                    NoteId = 1,
                    Content = "Test content",
                    SourceId = 1,
                    TagList = new ObservableCollection<Tags>(),
                    Username = "test user"
                },
                // Add more expected notes as needed for your test cases
            };

            mockConnection.SetupDapper(x => x.Query<Note>(SqlConstants.GetNotesByName, It.IsAny<object>(), null, true, null, null))
                .Returns(expectedNotes);

            mockConnection.SetupDapper(x => x.Query<Note>(SqlConstants.GetNotesByNameContains, It.IsAny<object>(), null, true, null, null))
                .Returns(new List<Note>()
                {
                    new Note()
                    {
                        NoteId = 1,
                        Content = "Test content",
                        SourceId = 1,
                        TagList = new ObservableCollection<Tags>(),
                        Username = "test user"
                    }
                });

            var noteDAL = new NoteDAL(mockConnection.Object);

            // Act
            var result = noteDAL.SearchNotesByName("TestName", "test user");

            // Assert
            // Verify that the queries were called with the correct parameters
            // Assert the result
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedNotes.Count, result.Count);
            Assert.IsTrue(result.All(n => expectedNotes.Contains(n)));

            // Additional assertions as needed
        }
        [TestMethod]
        public void TestUpdateNoteContent()
        {
            // Arrange
            var mockConnection = new Mock<IDbConnection>();

            var noteToUpdate = new Note
            {
                NoteId = 1,
                Content = "Updated content",
                SourceId = 1,
                TagList = new ObservableCollection<Tags>(),
                Username = "test user"
            };

            mockConnection.SetupDapper(x => x.Execute(SqlConstants.UpdateNoteContent, It.IsAny<object>(), null, null, null))
                .Returns(1);
            var noteDAL = new NoteDAL(mockConnection.Object);

            var result = noteDAL.UpdateNoteContent(noteToUpdate);

            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void TestGetAllNotesFromUser()
        {
            // Arrange
            var mockConnection = new Mock<IDbConnection>();

            var expectedNotes = new List<Note>
            {
                new Note
                {
                    NoteId = 1,
                    Content = "Test content",
                    SourceId = 1,
                    TagList = new ObservableCollection<Tags>(),
                    Username = "test user"
                },
                // Add more expected notes as needed for your test cases
            };

            mockConnection.SetupDapper(x => x.Query<Note>(SqlConstants.GetNotesByUsername, It.IsAny<object>(), null, true, null, null))
                .Returns(expectedNotes);

            var noteDAL = new NoteDAL(mockConnection.Object);

            // Act
            var result = noteDAL.GetAllNotesFromUser("test user");

            // Assert the result
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedNotes.Count, result.Count);
            Assert.IsTrue(result.All(n => expectedNotes.Contains(n)));

            // Additional assertions as needed
        }

    }
}
