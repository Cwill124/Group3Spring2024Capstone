using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;

namespace CapstoneASP.Database.Repository;

public interface INoteRepository
{
    #region Methods

    public Task Create(Note note);

    public Task<IEnumerable<Note>> GetNotesBySource(int sourceId);

    public Task Delete(int noteId);

    #endregion
}

public class NoteRepository : INoteRepository
{
    #region Data members

    private readonly DBContext.DBContext context;

    #endregion

    #region Constructors

    #region Constructor

    public NoteRepository(DBContext.DBContext context)
    {
        this.context = context;
    }

    #endregion

    #endregion

    #region Methods

    public async Task Create(Note note)
    {
        using var connection = this.context.Connection;

        await connection.ExecuteAsync(SqlConstants.CreateNote, note);
    }

    public async Task<IEnumerable<Note>> GetNotesBySource(int sourceId)
    {
        using var connection = this.context.Connection;

        var dyResult = await connection.QueryAsync<dynamic>(SqlConstants.GetNotesBySourceId, new {sourceId});
        var notes = new List<Note>();

        foreach (var item in dyResult)
        {
            var note = new Note
            {
                NoteId = item.note_id,
                Content = item.content,
                SourceId = item.source_id,
                Username = item.username
            };
            notes.Add(note);
        }
        return notes;
    }

    public async Task Delete(int noteId)
    {
        var connection = this.context.Connection;

        await connection.ExecuteAsync(SqlConstants.DeleteNote, new { noteId });
    }

    #endregion
}