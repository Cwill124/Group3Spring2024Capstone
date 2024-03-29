using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneASP.Controllers
{
    /// <summary>
    /// Controller for handling all Note API requests
    /// </summary>
    [Route("")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        #region Data members

        private readonly IConfiguration _configuration;
        private readonly INoteService noteService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration instance.</param>
        /// <param name="noteService">The note service instance.</param>
        public NoteController(IConfiguration configuration, INoteService noteService)
        {
            this._configuration = configuration;
            this.noteService = noteService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new note.
        /// if creating the note is successful returns OK else the exception is caught and a BadRequest is return with the note 
        /// object.
        /// </summary>
        /// <param name="note">The note to be created.</param>
        /// <returns>Returns IActionResult indicating the result of the operation.</returns>
        [HttpPost]
        [Route("Notes/Create")]
        public async Task<IActionResult> CreateNote([FromBody] Note note)
        {
            try
            {
                await this.noteService.Create(note);
                return Ok(note);
            }
            catch (Exception ex)
            {
                return BadRequest(note);
            }
        }

        /// <summary>
        /// Retrieves all the notes by the source id 
        /// </summary>
        /// <param name="id">The source ID to retrieve notes for.</param>
        /// <returns>Returns a collection of notes.</returns>
        [HttpPost]
        [Route("Notes/GetBySourceId")]
        public async Task<IEnumerable<Note>> GetBySourceId([FromBody] int id)
        {
            var notes = await this.noteService.GetNotesBySource(id);
            return notes;
        }

        /// <summary>
        /// Deletes a note by ID.
        /// </summary>
        /// <param name="id">The ID of the note to be deleted.</param>
        /// <returns>Returns a task representing the asynchronous operation.</returns>
        [HttpPost]
        [Route("Notes/Delete")]
        public async Task Delete([FromBody] int id)
        {
            await this.noteService.Delete(id);
        }

        /// <summary>
        /// Gets all notes by a user's username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Collection of all the notes created by a user</returns>
        [HttpPost]
        [Route("Notes/GetByUsername")]
        public async Task<IEnumerable<Note>> GetByNoteByUsername([FromBody] string username)
        {
            var notes = await this.noteService.GetNotesByUsername(username);
            return notes;
        }

        #endregion
    }
}
