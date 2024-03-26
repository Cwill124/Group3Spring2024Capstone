using CapstoneASP.Database.DBContext;
using CapstoneASP.Model;
using CapstoneASP.Util;


namespace CapstoneASP.Database.Repository
{
    /// <summary>
    /// Interface defining operations related to project repository.
    /// </summary>
    public interface IProjectRepository
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
    /// Repository class implementing IProjectRepository for project-related database operations.
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDataContext context;

        /// <summary>
        /// Constructor for initializing a new instance of the ProjectRepository class.
        /// </summary>
        /// <param name="context">The data context providing database connection.</param>
        public ProjectRepository(IDataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Retrieves all projects owned by a specific user.
        /// </summary>
        /// <param name="owner">The username of the owner.</param>
        /// <returns>A collection of projects owned by the specified user.</returns>
        public async Task<IEnumerable<Project>> GetAllProjectsForUser(string owner)
        {
            using var connection = await context.CreateConnection();

            var projects = await connection.QueryAsync<Project>(SqlConstants.GetAllProjectsByOwner, new { owner });

            return projects;
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="project">The project object containing details of the project to be created.</param>
        public async Task Create(Project project)
        {
            using var connection = await context.CreateConnection();

            await connection.ExecuteAsync(SqlConstants.CreateProject, project);
        }

        /// <summary>
        /// Deletes a project by its ID.
        /// </summary>
        /// <param name="id">The ID of the project to delete.</param>
        public async Task Delete(int id)
        {
            using var connection = await context.CreateConnection();
            await connection.ExecuteAsync(SqlConstants.DeleteProject, new { id });
        }

        /// <summary>
        /// Retrieves a project by its ID.
        /// </summary>
        /// <param name="id">The ID of the project to retrieve.</param>
        /// <returns>The project corresponding to the specified ID.</returns>
        public async Task<Project> GetProjectById(int id)
        {
            using var connection = await context.CreateConnection();

            var project = await connection.QueryAsync<Project>(SqlConstants.GetProjectById, new { id });

            return project.ElementAt(0);
        }
    }
}
