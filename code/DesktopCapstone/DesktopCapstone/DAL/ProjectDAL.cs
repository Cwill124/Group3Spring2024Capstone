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

        public void AddProject(Project project)
        {
            this.dbConnection.Open();
            this.dbConnection.Execute(SqlConstants.CreateProject, project);
            this.dbConnection.Close();
        }
    }
}
