using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CapstoneASP.Controllers
{
    [Route("")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        #region Data members

        private readonly IConfiguration _configuration;

        private readonly INoteService noteService;

        #endregion

        #region Constructors

        public NoteController(IConfiguration configuration, INoteService noteService)
        {
            this._configuration= configuration;
            this.noteService= noteService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [Route("Notes/Create")]
        public async Task<IActionResult> CreateNote([FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest(note);
            }

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
        [HttpPost]
        [Route("Notes/GetBySourceId")]
        public async Task<IEnumerable<Note>> GetBySourceId([FromBody] int id)
        {

            var notes = await this.noteService.GetNotesBySource(id);

            return notes;
        }

        #endregion
    }
}
