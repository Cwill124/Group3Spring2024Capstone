using CapstoneASP.Database.Repository;
using CapstoneASP.Model;

namespace CapstoneASP.Database.Service
{
    public interface INoteService
    {
        #region Methods

        public Task Create(Note note);

        public Task<IEnumerable<Note>> GetNotesBySource(int sourceId);

        #endregion
    }
    public class NoteService : INoteService
    {
        #region Data members

        private readonly INoteRepository repository;

        #endregion

        #region Constructor

        public NoteService(INoteRepository repository)
        {
            this.repository = repository;
        }

        #endregion
        public async Task Create(Note note)
        {
            await this.repository.Create(note);
        }

        public async Task<IEnumerable<Note>> GetNotesBySource(int sourceId)
        {
            var notes = await this.repository.GetNotesBySource(sourceId);

            return notes;
        }
    }
}
