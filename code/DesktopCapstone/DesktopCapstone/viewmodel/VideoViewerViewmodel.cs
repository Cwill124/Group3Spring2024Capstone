using DesktopCapstone.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCapstone.viewmodel
{
    public class VideoViewerViewmodel
    {
        private ObservableCollection<Source> sources;
        private string currentSourceLink;

        public VideoViewerViewmodel()
        {
            sources = new ObservableCollection<Source>();
        }
    }
}
