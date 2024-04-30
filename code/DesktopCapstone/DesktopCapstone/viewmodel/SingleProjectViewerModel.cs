using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibtexLibrary;
using DesktopCapstone.DAL;
using DesktopCapstone.model;

namespace DesktopCapstone.viewmodel
{
    /// <summary>
    /// This class handles the logic for the SingleProjectViewer window
    /// </summary>
    public class SingleProjectViewerModel
    {
        private ProjectDAL projectDAL;
        private string username;
        public Project Project { get; set; }
        public ObservableCollection<Source> ProjectSources { get; set; }
        public ObservableCollection<Source> UsersSources { get; set; }

        /// <summary>
        /// Creates a new instance of the SingleProjectViewerModel with the given project, DAL, and username
        /// </summary>
        /// <param name="project"> given project </param>
        /// <param name="dal"> given dal</param>
        /// <param name="username"> given username </param>
        public SingleProjectViewerModel(Project project, ProjectDAL dal, string username)
        {
            this.projectDAL = dal;
            this.Project = project;
            this.ProjectSources = new ObservableCollection<Source>();
            this.username = username;
            this.UsersSources = new ObservableCollection<Source>();
            this.LoadProjectSources();
            this.LoadUsersSources();
            Debug.WriteLine("Current project id: " + this.Project.ProjectId);
        }

        /// <summary>
        /// Loads the user's sources for the current project
        /// </summary>
        public void LoadProjectSources()
        {
            var projectSources = projectDAL.GetSourcesFromProject((int)Project.ProjectId);
            this.ProjectSources.Clear();
            foreach (var current in projectSources)
            {
                this.ProjectSources.Add(current);
            }
        }

        /// <summary>   
        /// Loads the user's sources not in the project
        /// </summary>
        public void LoadUsersSources()
        {
            this.UsersSources.Clear();
            var usersSources = projectDAL.GetSourcesNotInProject((int)this.Project.ProjectId, this.username);
            foreach (var current in usersSources)
            {
                this.UsersSources.Add(current);
            }
        }

        /// <summary>
        /// Removes the given sources from the project
        /// </summary>
        /// <param name="sources"> given sources </param>
        public void RemoveSourcesFromProject(List<Source> sources)
        {
            foreach (var current in sources)
            {
                projectDAL.RemoveSourceFromProject(this.Project.ProjectId.Value, current.SourceId.Value);
                this.UsersSources.Add(current);
                this.ProjectSources.Remove(current);

            }

        }

        /// <summary>
        /// Creates and return a string of the project sources
        /// </summary>
        /// <returns> an export string of sources </returns>
        public string CreateProjectSourcesExport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var source in ProjectSources)
            {
                sb.AppendLine($"@source{{");
                sb.AppendLine($"  SourceId = \"{source.SourceId}\",");
                sb.AppendLine($"  Name = \"{source.Name}\",");
                sb.AppendLine($"  Content = \"{source.Content}\",");
                if (!String.IsNullOrEmpty(source.MetaData))
                {
                    sb.AppendLine($"  MetaData = \"{source.MetaData}\",");
                }
                //sb.AppendLine($"  MetaData = \"{source.MetaData}\",");
                sb.AppendLine($"  SourceTypeId = \"{source.SourceTypeId}\",");
                if (!String.IsNullOrEmpty(source.Tags))
                {
                    sb.AppendLine($"  Tags = \"{source.Tags}\",");
                }
                //sb.AppendLine($"  Tags = \"{source.Tags}\",");
                sb.AppendLine($"  CreatedBy = \"{source.CreatedBy}\"");
                sb.AppendLine($"}}");
                sb.AppendLine();
            }

            return sb.ToString();

        }

        public void DeleteProject()
        {
            projectDAL.DeleteProject(this.Project);
        }
    }
}
