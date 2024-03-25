using CapstoneASP.Database.DBContext;
using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;

namespace CapstoneASP.Database.Repository
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsForUser();

        Task Create(Project project);

        Task Delete(int projectId);
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
        public Task<IEnumerable<Project>> GetAllProjectsForUser()
        {
            throw new NotImplementedException();
        }

        public async Task Create(Project project)
        {
            using var connection = await context.CreateConnection();

            await connection.ExecuteAsync(SqlConstants.CreateProject, project);
        }

        public Task Delete(int projectId)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
