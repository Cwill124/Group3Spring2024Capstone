using CapstoneASP.Database.DBContext;
using CapstoneASP.Model;
using CapstoneASP.Util;

namespace CapstoneASP.Database.Repository;

/// <summary>
///     Represents a repository interface for note-related operations.
/// </summary>
public interface INoteRepository
{
    #region Methods

    /// <summary>
    ///     Creates a new note with the specified details.
    /// </summary>
    /// <param name="note">The note details to be created.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task Create(Note note);

    /// <summary>
    ///     Retrieves notes associated with a specific source.
    /// </summary>
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
///     Represents a repository implementation for note-related operations.
/// </summary>
public class NoteRepository : INoteRepository
{
    #region Data members

    private readonly IDataContext context;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="NoteRepository" /> class with the specified database context.
    /// </summary>
    /// <param name="context">The database context used for repository operations.</param>
    public NoteRepository(IDataContext context)
    {
        this.context = context;
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public async Task Create(Note note)
    {
        using var connection = await this.context.CreateConnection();

        await connection.ExecuteAsync(SqlConstants.CreateNote, note);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Note>> GetNotesBySource(int sourceId)
    {
        using var connection = await this.context.CreateConnection();

        var dyResult = await connection.QueryAsync<Note>(SqlConstants.GetNotesBySourceId, new { sourceId });
        var notes = dyResult.ToList();

        return notes;
    }

    public async Task<IEnumerable<Note>> GetNotesByUsername(string username)
    {
        using var connection = await this.context.CreateConnection();

        var dyResult = await connection.QueryAsync<Note>(SqlConstants.GetNotesByUsername, new { username });
        var notes = dyResult.ToList();

        return notes;
    }

    /// <inheritdoc />
    public async Task Delete(int noteId)
    {
        var connection = await this.context.CreateConnection();

        await connection.ExecuteAsync(SqlConstants.DeleteNote, new { noteId });
    }

    #endregion
}