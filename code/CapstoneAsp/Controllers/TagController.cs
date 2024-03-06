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
    /// Controller for managing operations related to tags.
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

        [HttpPost]
        [Route("Tags/GetTagsBelongingToUsername")]
        public async Task<IEnumerable<Tags>> GetTagsBelongingToUsername([FromBody] string username)
        {
            var tags = await this.tagService.GetTagsBelongingToUser(username);
            return tags;
        }

        #endregion
    }
}

