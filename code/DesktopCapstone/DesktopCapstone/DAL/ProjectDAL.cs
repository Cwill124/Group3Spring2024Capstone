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
    public class ProjectDAL
    {
        private IDbConnection dbConnection;

        public ProjectDAL(IDbConnection connection)
        {
            // Enable Dapper to match property names with underscores in database columns
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            this.dbConnection = connection;
        }

        public ObservableCollection<Project> GetProjectsForUser(string username)
        {
            this.dbConnection.Open();
            var result = this.dbConnection.Query<Project>(SqlConstants.GetProjectsForUser, new { username });
            this.dbConnection.Close();
            return new ObservableCollection<Project>(result);
        }

        public ObservableCollection<Source> GetSourcesFromProject(int projectId)
        {
            this.dbConnection.Open();
            var result = this.dbConnection.Query<Source>(SqlConstants.GetSourcesByProjectId, new { projectId });
            this.dbConnection.Close();
            return new ObservableCollection<Source>(result);

        }

        public ObservableCollection<Source> GetSourcesNotInProject(int projectId, string username)
        {
            this.dbConnection.Open();
            var result = this.dbConnection.Query<Source>(SqlConstants.GetSourcesNotInProject, new { projectId , username});
            this.dbConnection.Close();
            return new ObservableCollection<Source>(result);
        }

        public void AddProject(Project project)
        {
            this.dbConnection.Open();
            this.dbConnection.Execute(SqlConstants.CreateProject, project);
            this.dbConnection.Close();
        }

        public void AddSourceToProject(int projectId, int sourceId)
        {
            this.dbConnection.Open();
            this.dbConnection.Execute(SqlConstants.AddSourceToProject, new { projectId, sourceId });
            this.dbConnection.Close();
        }

        public void RemoveSourceFromProject(int projectId, int sourceId)
        {
            this.dbConnection.Open();
            this.dbConnection.Execute(SqlConstants.RemoveSourceFromProject, new { projectId, sourceId });
            this.dbConnection.Close();
        }
    }
}
