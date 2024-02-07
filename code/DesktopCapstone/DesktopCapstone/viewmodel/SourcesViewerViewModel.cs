using desktop_capstone.DAL;
using DesktopCapstone.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCapstone.viewmodel
{
    public class SourcesViewerViewModel
    {
        private ObservableCollection<Source> sources;

        public SourcesViewerViewModel() {
        }

        private void initializeSources()
        {
            sources = new ObservableCollection<Source>();
            SourceDAL dal = new SourceDAL();
            
        }
    }
}
