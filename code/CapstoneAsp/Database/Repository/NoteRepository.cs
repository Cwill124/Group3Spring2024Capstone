using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneASP.Database.Repository
{
    /// <summary>
    /// Represents a repository interface for note-related operations.
    /// </summary>
    public interface INoteRepository
    {
        #region Methods

        /// <summary>
        /// Creates a new note with the specified details.
        /// </summary>
        /// <param name="note">The note details to be created.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Create(Note note);

        /// <summary>
        /// Retrieves notes associated with a specific source.
        /// </summary>
        /// <param name="sourceId">The identifier of the source.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a collection of notes.</returns>
        Task<IEnumerable<Note>> GetNotesBySource(int sourceId);

        /// <summary>
        /// Deletes a note based on its identifier.
        /// </summary>
        /// <param name="noteId">The identifier of the note to be deleted.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Delete(int noteId);

        #endregion
    }

    /// <summary>
    /// Represents a repository implementation for note-related operations.
    /// </summary>
    public class NoteRepository : INoteRepository
    {
        #region Data members

        private readonly DBContext.DBContext context;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteRepository"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context used for repository operations.</param>
        public NoteRepository(DBContext.DBContext context)
        {
            this.context = context;
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public async Task Create(Note note)
        {
            using var connection = this.context.Connection;

            await connection.ExecuteAsync(SqlConstants.CreateNote, note);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Note>> GetNotesBySource(int sourceId)
        {
            using var connection = this.context.Connection;

            var dyResult = await connection.QueryAsync<dynamic>(SqlConstants.GetNotesBySourceId, new { sourceId });
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

        /// <inheritdoc/>
        public async Task Delete(int noteId)
        {
            var connection = this.context.Connection;

            await connection.ExecuteAsync(SqlConstants.DeleteNote, new { noteId });
        }

        #endregion
    }
}
