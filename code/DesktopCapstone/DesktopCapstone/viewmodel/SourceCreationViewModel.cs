using desktop_capstone.DAL;
using DesktopCapstone.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DesktopCapstone.viewmodel
{
    public class SourceCreationViewModel
    {
        private ObservableCollection<SourceType> sourceTypes;
        private ObservableCollection<string> sourceFormat;

        public ObservableCollection<SourceType> SourceTypes { get { return this.sourceTypes; } }
        public ObservableCollection<string> SourceFormat { get { return this.sourceFormat; } }

        public SourceCreationViewModel() {
            this.sourceTypes = new ObservableCollection<SourceType>();
            this.sourceFormat = new ObservableCollection<string>();
            this.initializeLists();
        }


        private void initializeLists()
        {
            SourceDAL sourceDal = new SourceDAL();
            sourceTypes = sourceDal.getSourceTypes();

            sourceFormat = ["URL", "File"];
        }
    }
}
