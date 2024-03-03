using CapstoneASP.Controllers;
using CapstoneASP.Database.Repository;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using CapstoneASP.Tests.Context;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CapstoneASP.Tests.Controllers.NoteControllerTests;

public class NoteControllerTests
{
    #region Data members

    private INoteRepository noteRepository;

    private INoteService noteService;

    private NoteController noteController;
    
    private ITagRepository tagRepository;
    #endregion

    #region Methods

    [SetUp]
    public void SetUp()
    {
        var context = new MockDataContext();
        this.tagRepository = new TagRepository(context);
        this.noteRepository = new NoteRepository(context);
        this.noteService = new NoteService(this.noteRepository, tagRepository);
        this.noteController = new NoteController(null, this.noteService);
    }

    [Test]
    public void TestNotNull()
    {
        var noteController = new NoteController(null, this.noteService);

        Assert.IsNotNull(noteController);
    }

    [Test]
    public void CreateNoteValid()
    {
        var note = new Note
        {
            Content = "",
            Note_Id = 1,
            Source_Id = 1,
            Username = "User 1"
        };
        Assert.IsInstanceOfType<OkObjectResult>(this.noteController.CreateNote(note).Result);
    }

    [Test]
    public void CreateNoteInValid()
    {
        Assert.IsInstanceOfType<BadRequestObjectResult>(this.noteController.CreateNote(null).Result);
    }

    [Test]
    public async Task GetBySourceId()
    {
        var source = 1;
        var found = await this.noteController.GetBySourceId(source);

        var expected = MockDataContext.Notes.Where(x => x.Source_Id == source);

        Assert.AreEqual(expected.ToList().Count, found.Count());
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
        await this.noteController.Delete(note.Note_Id);
        Assert.IsFalse(MockDataContext.Notes.Contains(note));
    }

    #endregion
}