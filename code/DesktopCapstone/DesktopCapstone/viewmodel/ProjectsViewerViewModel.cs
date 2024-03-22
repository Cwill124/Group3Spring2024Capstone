using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCapstone.DAL;
using DesktopCapstone.model;

namespace DesktopCapstone.viewmodel
{
    public class ProjectsViewerViewModel
    {
        private string username;
        private ProjectDAL projectDAL;
        public ObservableCollection<Project> Projects { get; set; }

        public ProjectsViewerViewModel(string username, ProjectDAL dal)
        {
            this.username = username;
            this.projectDAL = dal;
            Projects = new ObservableCollection<Project>();
            RefreshProjects();
        }

        public void RefreshProjects()
        {
            this.Projects.Clear();
            var userProjects = projectDAL.GetProjectsForUser(username);
            foreach (var current in userProjects)
            {
                Projects.Add(current);
            }
        }
    }
}
