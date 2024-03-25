﻿using CapstoneASP.Database.Repository;
using CapstoneASP.Model;

namespace CapstoneASP.Database.Service
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsForUser(string owner);

        Task Create(Project project);

        Task Delete(int projectId);
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

        public Task Delete(int projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Project>> GetAllProjectsForUser(string owner)
        {
            return await this.projectRepository.GetAllProjectsForUser(owner);
        }
        #endregion

    }
}
