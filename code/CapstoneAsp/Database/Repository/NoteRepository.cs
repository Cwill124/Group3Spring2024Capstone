using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;

namespace CapstoneASP.Database.Repository
{
    public interface INoteRepository
    {
        #region Methods

        public Task Create(Note note);

        public Task<IEnumerable<Note>> GetNotesBySource(int sourceId);

        #endregion
    }
    public class NoteRepository : INoteRepository
    {
        #region Data members

        private readonly DBContext.DBContext context;

        #endregion

        #region Constructor

        public NoteRepository(DBContext.DBContext context)
        {
            this.context = context;
        }

        #endregion
        public async Task Create(Note note)
        {
            using var connection = context.Connection;

            await connection.ExecuteAsync(SqlConstants.CreateNote, note);
        }

        public Task<IEnumerable<Note>> GetNotesBySource(int sourceId)
        {
            throw new NotImplementedException();
        }
    }
}
