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
using DesktopCapstone.viewmodel;
using Moq;
using Moq.Dapper;

namespace DesktopTest.ViewModelTests
{
    [TestClass]
    public class ViewerViewModelTest
    {
        [TestMethod]
        public void TestSourceInitialization()
        {
            var mockSourceConnection = new Mock<IDbConnection>();
            var mockNoteConnection = new Mock<IDbConnection>();
            mockSourceConnection.SetupDapper(x => x.Query<Source>(SqlConstants.GetSourceById, null, null, true, null, null))
                .Returns(new List<Source>
                {
                    new Source
                    {
                        SourceId = 1,
                        Description = "test",
                        Name = "test",
                        Content = "{\"url\":\"https://www.youtube.com/watch?v=TwEq1e9YBxY\",\"file\":\" \"}",
                        MetaData = "test",
                        SourceTypeId = 1,
                        Tags = null,
                        CreatedBy = "test user"
                    }
                });
            mockNoteConnection.SetupDapper(x => x.Query<Note>(SqlConstants.GetNotesById, null, null, true, null, null))
                .Returns(new List<Note>
                {
                    new Note
                    {
                        SourceId = 1,
                        NoteId = 1,
                        Content = "{\"url\":\"https://www.youtube.com/watch?v=TwEq1e9YBxY\",\"file\":\" \"}",
                        Username = "test",
                        TagList = new ObservableCollection<Tags>(),
                    }
                });
            var sourceDal = new SourceDAL(mockSourceConnection.Object);
            var noteDal = new NoteDAL(mockNoteConnection.Object);
            var viewModel = new ViewerViewModel(1, 2 , noteDal, sourceDal);
            Assert.AreEqual(1, viewModel.CurrentSourceId);
        }

        [TestMethod]
        public void TestRefreshNotes()
        {
            var mockSourceConnection = new Mock<IDbConnection>();
            var mockNoteConnection = new Mock<IDbConnection>();
            mockSourceConnection.SetupDapper(x => x.Query<Source>(SqlConstants.GetSourceById, null, null, true, null, null))
                .Returns(new List<Source>
                {
                    new Source
                    {
                        SourceId = 1,
                        Description = "test",
                        Name = "test",
                        Content = "{\"url\":\"https://www.youtube.com/watch?v=TwEq1e9YBxY\",\"file\":\" \"}",
                        MetaData = "test",
                        SourceTypeId = 1,
                        Tags = null,
                        CreatedBy = "test user"
                    }
                });
            mockNoteConnection.SetupDapper(x => x.Query<Note>(SqlConstants.GetNotesById, null, null, true, null, null))
                .Returns(new List<Note>
                {
                    new Note
                    {
                        SourceId = 1,
                        NoteId = 1,
                        Content = "{\"url\":\"https://www.youtube.com/watch?v=TwEq1e9YBxY\",\"file\":\" \"}",
                        Username = "test",
                        TagList = new ObservableCollection<Tags>(),
                    }
                });
            var sourceDal = new SourceDAL(mockSourceConnection.Object);
            var noteDal = new NoteDAL(mockNoteConnection.Object);
            var viewModel = new ViewerViewModel(1, 2 , noteDal, sourceDal);
            viewModel.RefreshNotes();
            Assert.AreEqual(1, viewModel.Notes.Count);
        }
        
    }
}
