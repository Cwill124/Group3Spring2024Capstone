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

        public void ImportProject(string importText)
        {
            List<Source> sources = new List<Source>();

            // Define pattern for BibTeX entries
            string pattern = @"@source\{\s*SourceId = ""(?<SourceId>\d+)"",\s*Description = ""(?<Description>.+?)"",\s*Name = ""(?<Name>.+?)"",\s*Content = ""(?<Content>.+?)"",\s*MetaData = ""(?<MetaData>.+?)"",\s*SourceType = ""(?<SourceType>\d+)"",\s*Tags = ""(?<Tags>.+?)"",\s*CreatedBy = ""(?<CreatedBy>.+?)""\s*\}";

            // Parse BibTeX entries using regular expressions
            MatchCollection matches = Regex.Matches(importText, pattern, RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                Source source = new Source
                {
                    SourceId = int.Parse(match.Groups["SourceId"].Value),
                    Description = match.Groups["Description"].Value,
                    Name = match.Groups["Name"].Value,
                    Content = match.Groups["Content"].Value,
                    MetaData = match.Groups["MetaData"].Value,
                    SourceType = int.Parse(match.Groups["SourceType"].Value),
                    Tags = match.Groups["Tags"].Value,
                    CreatedBy = match.Groups["CreatedBy"].Value
                };
                sources.Add(source);
            }

            foreach (var current in sources)
            {
                if (current.CreatedBy != this.username)
                {
                    current.CreatedBy = this.username;
                    DALConnection.SourceDAL.CreateSource(current);
                }

            }

        }
    }
}
