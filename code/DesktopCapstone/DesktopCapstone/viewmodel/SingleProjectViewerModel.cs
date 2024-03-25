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
    public class SingleProjectViewerModel
    {
        private ProjectDAL projectDAL;
        private string username;
        public Project Project { get; set; }
        public ObservableCollection<Source> ProjectSources { get; set; }
        public ObservableCollection<Source> UsersSources { get; set; }

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

        public void LoadProjectSources()
        {
            this.ProjectSources.Clear();
            var projectSources = projectDAL.GetSourcesFromProject((int)Project.ProjectId);
            foreach (var current in projectSources)
            {
                this.ProjectSources.Add(current);
            }
        }

        public void LoadUsersSources()
        {
            this.UsersSources.Clear();
            var usersSources = projectDAL.GetSourcesNotInProject((int)this.Project.ProjectId, this.username);
            foreach (var current in usersSources)
            {
                this.UsersSources.Add(current);
            }
        }

        public void AddSourcesToProject(List<Source> sources)
        {
            foreach (var current in sources)
            {
                projectDAL.AddSourceToProject(this.Project.ProjectId.Value, current.SourceId.Value);
                this.ProjectSources.Add(current);
                this.UsersSources.Remove(current);
            }
            //this.LoadProjectSources();
            //this.LoadUsersSources();
        }

        public void RemoveSourcesFromProject(List<Source> sources)
        {
            foreach (var current in sources)
            {
                projectDAL.RemoveSourceFromProject(this.Project.ProjectId.Value, current.SourceId.Value);
                this.UsersSources.Add(current);
                this.ProjectSources.Remove(current);
                //this.UsersSources.Add(current);
            }
            //this.LoadProjectSources();
            //this.LoadUsersSources();
        }

        public string CreateProjectSourcesExport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var source in ProjectSources)
            {
                sb.AppendLine($"@source{{");
                sb.AppendLine($"  SourceId = \"{source.SourceId}\",");
                sb.AppendLine($"  Description = \"{source.Description}\",");
                sb.AppendLine($"  Name = \"{source.Name}\",");
                sb.AppendLine($"  Content = \"{source.Content}\",");
                sb.AppendLine($"  MetaData = \"{source.MetaData}\",");
                sb.AppendLine($"  SourceType = \"{source.SourceType}\",");
                sb.AppendLine($"  Tags = \"{source.Tags}\",");
                sb.AppendLine($"  CreatedBy = \"{source.CreatedBy}\"");
                sb.AppendLine($"}}");
                sb.AppendLine();
            }

            return sb.ToString();

        }


    }
}
