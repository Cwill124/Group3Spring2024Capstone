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

        [HttpPost]
        [Route("Project/GetAllByUser")]
        public async Task<IEnumerable<Project>> GetAllByUser([FromBody] string owner)
        {
            var projects = await this.projectService.GetAllProjectsForUser(owner);
            return projects;
        }

        [HttpPost]
        [Route("Project/GetById")]
        public async Task<Project> GetById([FromBody] int id)
        {
            var project = await this.projectService.GetProjectById(id);
            return project;
        }

        [HttpDelete]
        [Route("Project/Delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            try
            {
                await this.projectService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion
    }



}
