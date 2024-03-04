using CapstoneASP.Database.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneASP.Controllers
{
    [Route("")]
    [ApiController]
    public class TagController : ControllerBase
    {
        #region Data member

        private readonly IConfiguration config;
        
        private readonly ITagService tagService;


        #endregion

        #region Constructor

        public TagController(IConfiguration config, ITagService tagService)
        {
            this.config = config;
            this.tagService = tagService;
        }


        #endregion

        #region Methods


        [HttpPost]
        [Route("Tags/DeleteById")]
        public async Task<ActionResult> DeleteById([FromBody] int tagId)
        {
            try
            {
                await this.tagService.DeleteTagById(tagId);
                return Ok("Tag Deleted successfully");

            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete tag: {ex.Message}");
            }
        }


        #endregion
    }
}
