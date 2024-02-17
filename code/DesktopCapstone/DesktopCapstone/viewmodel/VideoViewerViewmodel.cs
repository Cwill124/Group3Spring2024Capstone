using desktop_capstone.DAL;
using DesktopCapstone.DAL;
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
        private ObservableCollection<Note> notes;
        private string currentSourceLink;

        public VideoViewerViewmodel()
        {
            this.InitializeLists();
            currentSourceLink = string.Empty;
        }

        private void InitializeLists()
        {
            SourceDAL sourceDal = new SourceDAL();
            NoteDAL noteDal = new NoteDAL();

            sources = sourceDal.GetAllSources();
        }
    }
}
