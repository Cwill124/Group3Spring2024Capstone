using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Service.NotesServiceTests;

public class NotesServiceTests
{
    #region Data members

    private INoteService noteService;

    private INoteRepository noteRepository;

    #endregion

    #region Methods

    [SetUp]
    public void Setup()
    {
        var context = new MockDataContext();

        this.noteRepository = new NoteRepository(context);

        this.noteService = new NoteService(this.noteRepository);
    }

    [Test]
    public void NotNullTests()
    {
        var noteService = new NoteService(this.noteRepository);

        Assert.IsNotNull(noteService);
    }

    [Test]
    public async Task GetNotesBySource()
    {
        var source = 1;
        var found = await this.noteService.GetNotesBySource(source);

        var expected = MockDataContext.Notes.Where(x => x.Source_Id == source);

        Assert.AreEqual(expected.ToList().Count, found.Count());
    }

    [Test]
    public async Task Create()
    {
        var newNote = new Note
        {
            Source_Id = 1,
            Content = "new",
            Note_Id = 5,
            Username = "User 1"
        };
        await this.noteService.Create(newNote);

        var expected = MockDataContext.Notes.Where(x => x.Note_Id == newNote.Note_Id);

        Assert.AreEqual(newNote.Note_Id, expected.ToList().ElementAt(0).Note_Id);
    }

    [Test]
    public async Task Delete()
    {
        var note = new Note
        {
            Content = "",
            Note_Id = 1,
            Source_Id = 1,
            Username = "User 1"
        };
        await this.noteService.Delete(note.Note_Id);
        Assert.IsFalse(MockDataContext.Notes.Contains(note));
    }

    #endregion
}