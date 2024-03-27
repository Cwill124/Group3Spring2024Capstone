using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Dapper;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using DesktopCapstone.util;
using DesktopCapstone.viewmodel;
using Moq;
using Moq.Dapper;

namespace DesktopTest.ViewModelTests
{
    [TestClass]
    public class NoteViewerViewModelTest
    {
        [TestMethod]
        public void TestRefreshNotes()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<Note>(SqlConstants.GetNotesByUsername, new { username = "test user" }, null, true, null, null))
                .Returns(new List<Note>
                {
                    new Note
                    {
                        NoteId = 1,
                        SourceId = 1,
                        Content = "test",
                        Username = "test user",
                        TagList = new ObservableCollection<Tags>()
                    }
                });
            var dal = new NoteDAL(mockConnection.Object);
            var viewModel = new NoteViewerViewModel(dal, "test user");
            viewModel.RefreshNotes();
            Assert.AreEqual(1, viewModel.Notes.Count);
        }

        [TestMethod]
        public void TestSearchNotesByName()
        {
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupDapper(x => x.Query<Note>(SqlConstants.GetNotesByName, new { name = "test" }, null, true, null, null))
                .Returns(new List<Note>
                {
                    new Note
                    {
                        NoteId = 1,
                        SourceId = 1,
                        Content = "test",
                        Username = "test user",
                        TagList = new ObservableCollection<Tags>()
                    },
                    new Note{
                        NoteId = 2,
                        SourceId = 2,
                        Content = "test",
                        Username = "test user2",
                        TagList = new ObservableCollection<Tags>()
                    }

                });
            var dal = new NoteDAL(mockConnection.Object);
            var viewModel = new NoteViewerViewModel(dal, "test user");
            viewModel.SearchNotesByName("test");
            Assert.AreEqual(2, viewModel.Notes.Count);

        }

        [TestMethod]
        public void TestGetAllTagsFromNotes()
        {
            var mockNoteConnection = new Mock<IDbConnection>();
            var mockTagConnection = new Mock<IDbConnection>();
            mockNoteConnection.SetupDapper(x => x.Query<Note>(SqlConstants.GetNotesByUsername, new { username = "test user" }, null, true, null, null))
                .Returns(new List<Note>
                {
                    new Note
                    {
                        NoteId = 1,
                        SourceId = 1,
                        Content = "test",
                        Username = "test user",
                        TagList = new ObservableCollection<Tags>()
                    }
                });
            mockTagConnection.SetupDapper(x => x.Query<Tags>(SqlConstants.GetTagsBelongingToUser, new { username = "test user" }, null, true, null, null))
                .Returns(new List<Tags>
                {
                    new Tags
                    {
                        TagId = 1,
                        Tag = "test",
                        Note = 1
                    }
                });
            var noteDal = new NoteDAL(mockNoteConnection.Object);
            var tagDal = new TagDAL(mockTagConnection.Object);
            var viewModel = new NoteViewerViewModel(noteDal, "test user");
            var tags = viewModel.GetAllTagsFromNotes(tagDal);
            Assert.AreEqual(1, tags.Count);
        }

        [TestMethod]
        public void TestFilterNotesByTag()
        {
            var mockNoteConnection = new Mock<IDbConnection>();
            var mockTagConnection = new Mock<IDbConnection>();
            mockNoteConnection.SetupDapper(x => x.Query<Note>(SqlConstants.GetNotesByUsername, new { username = "test user" }, null, true, null, null))
                .Returns(new List<Note>
                {
                    new Note
                    {
                        NoteId = 1,
                        SourceId = 1,
                        Content = "test",
                        Username = "test user",
                        TagList = new ObservableCollection<Tags>()
                    }
                });
            mockTagConnection.SetupDapper(x => x.Query<Tags>(SqlConstants.GetTagsBelongingToUser, new { username = "test user" }, null, true, null, null))
                .Returns(new List<Tags>
                {
                    new Tags
                    {
                        TagId = 1,
                        Tag = "test",
                        Note = 1
                    }
                });
            var noteDal = new NoteDAL(mockNoteConnection.Object);
            var tagDal = new TagDAL(mockTagConnection.Object);
            var viewModel = new NoteViewerViewModel(noteDal, "test user");
            var tags = viewModel.GetAllTagsFromNotes(tagDal);
            viewModel.FilteredTags = tags;
            viewModel.FilterNotesByTag();
            Assert.AreEqual(0, viewModel.Notes.Count);

        }

        public void TestRemoveTagFromFilter()
        {
            var mockNoteConnection = new Mock<IDbConnection>();
            var mockTagConnection = new Mock<IDbConnection>();
            mockNoteConnection.SetupDapper(x => x.Query<Note>(SqlConstants.GetNotesByUsername, new { username = "test user" }, null, true, null, null))
                .Returns(new List<Note>
                {
                    new Note
                    {
                        NoteId = 1,
                        SourceId = 1,
                        Content = "test",
                        Username = "test user",
                        TagList = new ObservableCollection<Tags>()
                    }
                });
            mockTagConnection.SetupDapper(x => x.Query<Tags>(SqlConstants.GetTagsBelongingToUser, new { username = "test user" }, null, true, null, null))
                .Returns(new List<Tags>
                {
                    new Tags
                    {
                        TagId = 1,
                        Tag = "test",
                        Note = 1
                    }
                });
            var noteDal = new NoteDAL(mockNoteConnection.Object);
            var tagDal = new TagDAL(mockTagConnection.Object);
            var viewModel = new NoteViewerViewModel(noteDal, "test user");
            var tags = viewModel.GetAllTagsFromNotes(tagDal);
            viewModel.FilteredTags = tags;
            viewModel.RemoveTagFromFilter(tags[0]);
            Assert.AreEqual(0, viewModel.FilteredTags.Count);
        }

    }
}
