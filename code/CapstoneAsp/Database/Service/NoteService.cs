using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneASP.Database.Service
{
    /// <summary>
    /// Represents a service interface for note-related operations.
    /// </summary>
    public interface INoteService
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
    /// Represents a service implementation for note-related operations.
    /// </summary>
    public class NoteService : INoteService
    {
        #region Data members

        private readonly INoteRepository repository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteService"/> class with the specified repository.
        /// </summary>
        /// <param name="repository">The repository for note-related operations.</param>
        public NoteService(INoteRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public async Task Create(Note note)
        {
            await this.repository.Create(note);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Note>> GetNotesBySource(int sourceId)
        {
            var notes = await this.repository.GetNotesBySource(sourceId);

            return notes;
        }

        /// <inheritdoc/>
        public async Task Delete(int noteId)
        {
            await this.repository.Delete(noteId);
        }

        #endregion
    }
}
