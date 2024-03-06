using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CapstoneASP.Database.Service;

/// <summary>
///     Represents a service interface for note-related operations.
/// </summary>
public interface INoteService
{
    #region Methods

    /// <summary>
    ///     Creates a new note with the specified details.
    ///     Creates Tags in the DB that are associated with the new Note
    /// </summary>
    /// <param name="note">The note details to be created.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task Create(Note note);

    /// <summary>
    ///     Retrieves notes and their tags associated with a specific source.
    /// </summary>
    /// <param name="sourceId">The identifier of the source.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation, containing a collection of notes.</returns>
    Task<IEnumerable<Note>> GetNotesBySource(int sourceId);

    Task<IEnumerable<Note>> GetNotesByUsername(string username);

    /// <summary>
    ///     Deletes a note based on its identifier.
    /// </summary>
    /// <param name="noteId">The identifier of the note to be deleted.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task Delete(int noteId);

    #endregion
}

/// <summary>
///     Represents a service implementation for note-related operations.
/// </summary>
public class NoteService : INoteService
{
    #region Data members

    private readonly INoteRepository repository;

    private readonly ITagRepository tagRepository;
    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="NoteService" /> class with the specified repository.
    /// </summary>
    /// <param name="repository">The repository for note-related operations.</param>
    public NoteService(INoteRepository repository, ITagRepository tagRepository)
    {
        this.repository = repository;
        this.tagRepository = tagRepository;
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public async Task Create(Note note)
    {
        var tags = JArray.Parse(note.Tags);
        var newNote = await this.repository.Create(note);
        foreach (var tag in tags)
        {
            var newTag = new Tags()
            {
                Tag = tag.ToString(),
                Note = newNote.Note_Id

            };
            await this.tagRepository.CreateTag(newTag);
        }



    }

    /// <inheritdoc />
    public async Task<IEnumerable<Note>> GetNotesBySource(int sourceId)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = false
        };
        var notes = await this.repository.GetNotesBySource(sourceId);
        foreach (var note in notes)
        {
            var tags = await this.tagRepository.GetTagsByNoteId(note.Note_Id);
            note.Tags = JsonSerializer.Serialize(tags, options);
        }

        return notes;
    }

    /// <inheritdoc />
    public async Task Delete(int noteId)
    {
        await this.repository.Delete(noteId);
    }

    public async Task<IEnumerable<Note>> GetNotesByUsername(string username)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = false
        };
        var notes = await this.repository.GetNotesByUsername(username);
        foreach (var note in notes)
        {
            var tags = await this.tagRepository.GetTagsByNoteId(note.Note_Id);
            note.Tags = JsonSerializer.Serialize(tags, options);
        }
        return notes;
    }
    #endregion
}