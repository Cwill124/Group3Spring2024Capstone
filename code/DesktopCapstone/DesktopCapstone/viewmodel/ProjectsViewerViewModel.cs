using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DesktopCapstone.DAL;
using DesktopCapstone.model;

namespace DesktopCapstone.viewmodel
{
 
    /// <summary>
    /// This class handles the logic for the ProjectsViewer window
    /// </summary>
    public class ProjectsViewerViewModel
    {
        private string username;
        private ProjectDAL projectDAL;

        /// <summary>
        /// The User's projects
        /// </summary>
        public ObservableCollection<Project> Projects { get; set; }

        /// <summary>
        /// Creates a new instance of the ProjectsViewerViewModel with the given username and DAL
        /// </summary>
        /// <param name="username"> the given username </param>
        /// <param name="dal"> the given dal </param>
        public ProjectsViewerViewModel(string username, ProjectDAL dal)
        {
            this.username = username;
            this.projectDAL = dal;
            Projects = new ObservableCollection<Project>();
            RefreshProjects();
        }

        /// <summary>
        /// Gets the projects for the user
        /// </summary>
        public void RefreshProjects()
        {
            this.Projects.Clear();
            var userProjects = projectDAL.GetProjectsForUser(username);
            foreach (var current in userProjects)
            {
                Projects.Add(current);
            }
        }

        public void DeleteProject(Project project)
        {
            projectDAL.DeleteProject(project); ;

        }

    }
}
