using CapstoneASP.Database.Repository;
using CapstoneASP.Model;

namespace CapstoneASP.Database.Service;

public interface INoteService
{
    #region Methods

    public Task Create(Note note);

    public Task<IEnumerable<Note>> GetNotesBySource(int sourceId);

    public Task Delete(int noteId);

    #endregion
}

public class NoteService : INoteService
{
    #region Data members

    private readonly INoteRepository repository;

    #endregion

    #region Constructors

    #region Constructor

    public NoteService(INoteRepository repository)
    {
        this.repository = repository;
    }

    #endregion

    #endregion

    #region Methods

    public async Task Create(Note note)
    {
        await this.repository.Create(note);
    }

    public async Task<IEnumerable<Note>> GetNotesBySource(int sourceId)
    {
        var notes = await this.repository.GetNotesBySource(sourceId);

        return notes;
    }

    public async Task Delete(int noteId)
    {
        await this.repository.Delete(noteId);
    }

    #endregion
}