using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopCapstone.model;

namespace DesktopCapstone.viewmodel
{
    public class SingleProjectViewerModel
    {
        public Project Project { get; set; }
        public ObservableCollection<Source> ProjectSources { get; set; }

        public SingleProjectViewerModel(Project project)
        {
            Project = project;
            ProjectSources = new ObservableCollection<Source>();
        }

        private void LoadProjectSources()
        {

        }

    }
}
