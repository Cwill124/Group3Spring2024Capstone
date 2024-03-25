using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneASP.Controllers
{
    /// <summary>
    /// Controller for handling project-related operations.
    /// </summary>
    [Route("")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IConfiguration configuration;
        private IProjectService projectService;

        /// <summary>
        /// Constructor for initializing a new instance of the ProjectController class.
        /// </summary>
        /// <param name="configuration">The configuration object for accessing application settings.</param>
        /// <param name="projectService">The service responsible for project-related operations.</param>
        public ProjectController(IConfiguration configuration, IProjectService projectService)
        {
            this.configuration = configuration;
            this.projectService = projectService;
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="project">The project object containing details of the project to be created.</param>
        /// <returns>The created project if successful, otherwise BadRequest with error message.</returns>
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

        /// <summary>
        /// Retrieves all projects owned by a specific user.
        /// </summary>
        /// <param name="owner">The username of the owner.</param>
        /// <returns>A list of projects owned by the specified user.</returns>
        [HttpPost]
        [Route("Project/GetAllByUser")]
        public async Task<IEnumerable<Project>> GetAllByUser([FromBody] string owner)
        {
            var projects = await this.projectService.GetAllProjectsForUser(owner);
            return projects;
        }

        /// <summary>
        /// Retrieves a project by its ID.
        /// </summary>
        /// <param name="id">The ID of the project to retrieve.</param>
        /// <returns>The project corresponding to the specified ID.</returns>
        [HttpPost]
        [Route("Project/GetById")]
        public async Task<Project> GetById([FromBody] int id)
        {
            var project = await this.projectService.GetProjectById(id);
            return project;
        }

        /// <summary>
        /// Deletes a project by its ID.
        /// </summary>
        /// <param name="id">The ID of the project to delete.</param>
        /// <returns>Ok if deletion is successful, otherwise an error message.</returns>
        [HttpDelete]
        [Route("Project/Delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            
            await this.projectService.Delete(id);
            return Ok();
                
        }
    }
}
