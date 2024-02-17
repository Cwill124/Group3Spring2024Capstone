using desktop_capstone.DAL;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using System.Collections.ObjectModel;

namespace DesktopCapstone.viewmodel
{
    /// <summary>
    /// View model for the VideoViewer window, providing data for source and note management.
    /// </summary>
    public class VideoViewerViewmodel
    {
        private ObservableCollection<Source> sources;
        private ObservableCollection<Note> notes;
        private string currentSourceLink;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoViewerViewmodel"/> class with default values.
        /// </summary>
        public VideoViewerViewmodel()
        {
            this.InitializeLists();
            currentSourceLink = string.Empty;
        }

        /// <summary>
        /// Initializes the collections of sources and notes.
        /// </summary>
        private void InitializeLists()
        {
            SourceDAL sourceDal = new SourceDAL();
            NoteDAL noteDal = new NoteDAL();

            sources = sourceDal.GetAllSources();
        }
    }
}