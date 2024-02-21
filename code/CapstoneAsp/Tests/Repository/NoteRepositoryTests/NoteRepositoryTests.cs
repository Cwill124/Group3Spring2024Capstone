using CapstoneASP.Database.DBContext;
using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Repository.NoteRepositoryTests
{
    public class NoteRepositoryTests
    {
        #region Data members

        private IDataContext context;

        private INoteRepository repository;

        #endregion

        #region Methods

        [SetUp]
        public void Setup()
        {
            context = new MockDataContext();
            repository = new NoteRepository(this.context);
        }

        [Test]
        public void NotNullTest()
        {
            var noteRepo = new NoteRepository(this.context);

            Assert.IsNotNull(noteRepo);
        }
        [Test]
        public async Task GetNotesBySource()
        {
            var source = 1;
            var found = await this.repository.GetNotesBySource(source);

            var expected = MockDataContext.Notes.Where(x => x.Source_Id == source);

            Assert.AreEqual(expected.ToList().Count, found.Count());
        }

        [Test]
        public async Task Create()
        {
            var newNote = new Note()
            {
                Source_Id = 1,
                Content = "new",
                Note_Id = 5,
                Username = "User 1"
            };
            await this.repository.Create(newNote);

            var expected = MockDataContext.Notes.Where(x => x.Note_Id == newNote.Note_Id);

            Assert.AreEqual(newNote.Note_Id, expected.ToList().ElementAt(0).Note_Id);
        }

        [Test]
        public async Task Delete()
        {
            var note = new Note()
            {
                Content = "",
                Note_Id = 1,
                Source_Id = 1,
                Username = "User 1"
            };
            await this.repository.Delete(note.Note_Id);
            Assert.IsFalse(MockDataContext.Notes.Contains(note));
        }

        #endregion
    }
}
