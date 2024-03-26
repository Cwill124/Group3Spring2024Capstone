using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneASP.Database.Service
{
    /// <summary>
    /// Interface defining operations related to project services.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Retrieves all projects owned by a specific user.
        /// </summary>
        /// <param name="owner">The username of the owner.</param>
        /// <returns>A collection of projects owned by the specified user.</returns>
        Task<IEnumerable<Project>> GetAllProjectsForUser(string owner);

        /// <summary>
        /// Retrieves a project by its ID.
        /// </summary>
        /// <param name="id">The ID of the project to retrieve.</param>
        /// <returns>The project corresponding to the specified ID.</returns>
        Task<Project> GetProjectById(int id);

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="project">The project object containing details of the project to be created.</param>
        Task Create(Project project);

        /// <summary>
        /// Deletes a project by its ID.
        /// </summary>
        /// <param name="id">The ID of the project to delete.</param>
        Task Delete(int id);

       
    }

    /// <summary>
    /// Service class implementing IProjectService for project-related operations.
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;

        /// <summary>
        /// Constructor for initializing a new instance of the ProjectService class.
        /// </summary>
        /// <param name="projectRepository">The project repository for database operations.</param>
        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        /// <inheritdoc />
        public async Task Create(Project project)
        {
            await this.projectRepository.Create(project);
        }

        /// <inheritdoc />
        public async Task Delete(int id)
        {
            await this.projectRepository.Delete(id);
        }



        /// <inheritdoc />
        public async Task<IEnumerable<Project>> GetAllProjectsForUser(string owner)
        {
            return await this.projectRepository.GetAllProjectsForUser(owner);
        }

        /// <inheritdoc />
        public async Task<Project> GetProjectById(int id)
        {
            return await this.projectRepository.GetProjectById(id);
        }
    }
}
