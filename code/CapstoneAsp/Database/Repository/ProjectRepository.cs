using CapstoneASP.Database.DBContext;
using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;

namespace CapstoneASP.Database.Repository
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsForUser(string owner);

        Task<Project> GetProjectById(int id);

        Task Create(Project project);

        Task Delete(int id);
    }
    public class ProjectRepository : IProjectRepository
    {
        #region Data members

        private readonly IDataContext context;
        #endregion

        #region Constructors

        public ProjectRepository(IDataContext context)
        {
            this.context = context;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<Project>> GetAllProjectsForUser(string owner)
        {
            using var connection = await context.CreateConnection();

            var projects = await connection.QueryAsync<Project>(SqlConstants.GetAllProjectsByOwner, new { owner });

            return projects;

        }

        public async Task Create(Project project)
        {
            using var connection = await context.CreateConnection();

            await connection.ExecuteAsync(SqlConstants.CreateProject, project);
        }

        public async Task Delete(int id)
        {
            using var connection = await context.CreateConnection();
            await connection.ExecuteAsync(SqlConstants.DeleteProject, new { id });
        }

        public async Task<Project> GetProjectById(int id)
        {
            using var connection = await context.CreateConnection();

            var project = await connection.QueryAsync<Project>(SqlConstants.GetProjectById, new { id });

            return project.ElementAt(0);
        }
        #endregion


    }
}
