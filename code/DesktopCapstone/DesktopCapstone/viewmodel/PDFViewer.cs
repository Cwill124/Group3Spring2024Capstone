using DesktopCapstone.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCapstone.viewmodel
{
    public class PDFViewer
    {

        private ObservableCollection<Source> sources;
        private string currentSourceLink;

        public PDFViewer() {
            sources = new ObservableCollection<Source>();
            currentSourceLink = string.Empty;
        }


    }
}
