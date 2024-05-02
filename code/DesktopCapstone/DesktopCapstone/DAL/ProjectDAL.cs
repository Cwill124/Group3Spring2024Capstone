using Dapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCapstone.model;
using DesktopCapstone.util;

namespace DesktopCapstone.DAL
{
    /// <summary>
    /// The ProjectDAL class is responsible for handling all database access for the Project model
    /// </summary>
    public class ProjectDAL
    {
        private IDbConnection dbConnection;

        /// <summary>
        /// Creates a new ProjectDAL with the specified connection
        /// </summary>
        /// <param name="connection"></param>
        public ProjectDAL(IDbConnection connection)
        {
            // Enable Dapper to match property names with underscores in database columns
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            this.dbConnection = connection;
        }

        /// <summary>
        /// Gets all projects for the specified username
        /// </summary>
        /// <param name="username"> the given username</param>
        /// <returns></returns>
        public ObservableCollection<Project> GetProjectsForUser(string username)
        {
            this.dbConnection.Open();
            var result = this.dbConnection.Query<Project>(SqlConstants.GetProjectsForUser, new { username });
            this.dbConnection.Close();
            return new ObservableCollection<Project>(result);
        }

        /// <summary>
        /// Gets all sources for the specified project id
        /// </summary>
        /// <param name="projectId"> given project id</param>
        /// <returns></returns>
        public ObservableCollection<Source> GetSourcesFromProject(int projectId)
        {
            this.dbConnection.Open();
            var result = this.dbConnection.Query<Source>(SqlConstants.GetSourcesByProjectId, new { projectId });
            this.dbConnection.Close();
            return new ObservableCollection<Source>(result);

        }

        /// <summary>
        /// Gets all sources that are not in the specified project id
        /// </summary>
        /// <param name="projectId"> given project id </param>
        /// <param name="username"> the given username</param>
        /// <returns></returns>
        public ObservableCollection<Source> GetSourcesNotInProject(int projectId, string username)
        {
            this.dbConnection.Open();
            var result = this.dbConnection.Query<Source>(SqlConstants.GetSourcesNotInProject, new { projectId , username});
            this.dbConnection.Close();
            return new ObservableCollection<Source>(result);
        }

        /// <summary>
        /// Adds a new project to the database
        /// </summary>
        /// <param name="project"> the new project </param>
        public int AddProject(Project project)
        {
            this.dbConnection.Open();
            var result = this.dbConnection.Execute(SqlConstants.CreateProject, project);
            this.dbConnection.Close();
            return result;
        }

        /// <summary>
        /// Adds a source to the specified project
        /// </summary>
        /// <param name="projectId"> given project id </param>
        /// <param name="sourceId"> given source id </param>
        public int AddSourceToProject(int projectId, int sourceId)
        {
            this.dbConnection.Open();
            var result = this.dbConnection.Execute(SqlConstants.AddSourceToProject, new { projectId, sourceId });
            this.dbConnection.Close();
            return result;
        }

        /// <summary>
        /// Removes the specified source from the specified project
        /// </summary>
        /// <param name="projectId"> the given project id </param>
        /// <param name="sourceId"> the given source id</param>
        public int RemoveSourceFromProject(int projectId, int sourceId)
        {
            this.dbConnection.Open();
            var result = this.dbConnection.Execute(SqlConstants.RemoveSourceFromProject, new { projectId, sourceId });
            this.dbConnection.Close();
            return result;
        }

        public int DeleteProject(Project project)
        {
            if (project != null)
            {
                this.dbConnection.Open();
                var result = this.dbConnection.Execute(SqlConstants.DeleteProject, project);
                this.dbConnection.Close();
                return result;
            }
            
            return 0;
        }
    }
}
