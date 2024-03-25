using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneASP.Controllers
{
    [Route("")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        #region Data members

        private IConfiguration configuration;

        private IProjectService projectService;

        #endregion

        #region Constructor

        public ProjectController(IConfiguration configuration, IProjectService projectService)
        {
            this.configuration = configuration;
            this.projectService = projectService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [Route("Project/Create")]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            try
            {
                await this.projectService.Create(project);
                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }



}
