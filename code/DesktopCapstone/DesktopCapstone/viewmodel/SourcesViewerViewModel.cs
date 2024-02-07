using desktop_capstone.DAL;
using DesktopCapstone.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCapstone.viewmodel
{
    public class SourcesViewerViewModel
    {
        private ObservableCollection<Source> sources;

        public ObservableCollection<Source> Sources {  get { return sources; } }

        public SourcesViewerViewModel() {
            this.sources = new ObservableCollection<Source>();
            this.initializeSources();
            Debug.WriteLine(this.sources.Count);
        }

        public void refreshSources()
        {
            this.sources.Clear();
            SourceDAL dal = new SourceDAL();
            foreach (Source source in dal.getAllSources()) {
                this.sources.Add(source);
            }
            
        }

        private void initializeSources()
        {
            SourceDAL dal = new SourceDAL();
            this.sources = dal.getAllSources();
            
        }


    }
}
