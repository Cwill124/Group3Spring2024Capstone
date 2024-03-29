using CapstoneASP.Database.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using CapstoneASP.Model;

namespace CapstoneASP.Controllers
{
    /// <summary>
    /// Controller for tags handling all api calls 
    /// </summary>
    [Route("")]
    [ApiController]
    public class TagController : ControllerBase
    {
        #region Data Members

        private readonly IConfiguration config;
        private readonly ITagService tagService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TagController"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="tagService">The tag service.</param>
        public TagController(IConfiguration config, ITagService tagService)
        {
            this.config = config;
            this.tagService = tagService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes a tag by its ID.
        /// </summary>
        /// <param name="tagId">The ID of the tag to delete.</param>
        /// <returns>An action result indicating success or failure.</returns>
        [HttpPost]
        [Route("Tags/DeleteById")]
        public async Task<ActionResult> DeleteById([FromBody] int tagId)
        {
            await this.tagService.DeleteTagById(tagId);
            return Ok("Tag Deleted successfully");
        }

        /// <summary>
        /// Retrieves a collection of tags belonging to a specific username.
        /// </summary>
        /// <param name="username">The username for which to retrieve tags.</param>
        /// <returns>An asynchronous task that represents the operation, returning a collection of Tags.</returns>
        [HttpPost]
        [Route("Tags/GetTagsBelongingToUsername")]
        public async Task<IEnumerable<Tags>> GetTagsBelongingToUsername([FromBody] string username)
        {
            var tags = await this.tagService.GetTagsBelongingToUser(username);
            return tags;
        }

        /// <summary>
        /// Creates a new tag using the provided Tag object.
        /// </summary>
        /// <param name="tag">The Tag object containing information about the new tag.</param>
        /// <returns>An asynchronous task that represents the operation, returning an IActionResult with a success message.</returns>
        [HttpPost]
        [Route("Tags/CreateTag")]
        public async Task<IActionResult> CreateTag([FromBody] Tags tag)
        {
            await this.tagService.CreateTag(tag);
            return Ok("Tag created successfully");
        }

        /// <summary>
        /// Retrieves a collection of tags associated with a specific note ID.
        /// </summary>
        /// <param name="noteId">The ID of the note for which to retrieve tags.</param>
        /// <returns>An asynchronous task that represents the operation, returning a collection of Tags.</returns>
        [HttpPost]
        [Route("Tags/GetByNoteId")]
        public async Task<IEnumerable<Tags>> GetTagsByNoteId([FromBody] int noteId)
        {
            var tags = await this.tagService.GetTagsByNoteId(noteId);
            return tags;
        }

        #endregion
    }
}

