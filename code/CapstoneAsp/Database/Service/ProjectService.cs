using CapstoneASP.Database.Repository;
using CapstoneASP.Model;

namespace CapstoneASP.Database.Service
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsForUser(string owner);

        Task<Project> GetProjectById(int id);

        Task Create(Project project);

        Task Delete(int id);
    }
    public class ProjectService : IProjectService
    {
        #region Data members

        private readonly IProjectRepository projectRepository;

        #endregion

        #region Constructor

        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        #endregion

        #region Methods
        public async Task Create(Project project)
        {
            await this.projectRepository.Create(project);
        }

        public async Task Delete(int id)
        {
            await this.projectRepository.Delete(id);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsForUser(string owner)
        {
            return await this.projectRepository.GetAllProjectsForUser(owner);
        }

        public async Task<Project> GetProjectById(int id)
        {
            return await this.projectRepository.GetProjectById(id);
        }
        #endregion

    }
}
