using desktop_capstone.DAL;
using DesktopCapstone.model;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DesktopCapstone.viewmodel
{
    /// <summary>
    /// View model for the SourcesViewer window, providing data for source management.
    /// </summary>
    public class SourcesViewerViewModel
    {
        private ObservableCollection<Source> sources;

        /// <summary>
        /// Gets the collection of sources.
        /// </summary>
        public ObservableCollection<Source> Sources { get { return sources; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesViewerViewModel"/> class with default values.
        /// </summary>
        public SourcesViewerViewModel()
        {
            this.sources = new ObservableCollection<Source>();
            this.InitializeSources();
            Debug.WriteLine(this.sources.Count);
        }

        /// <summary>
        /// Refreshes the collection of sources from the data source.
        /// </summary>
        public void RefreshSources()
        {
            this.sources.Clear();
            SourceDAL dal = new SourceDAL();
            foreach (Source source in dal.GetAllSources())
            {
                this.sources.Add(source);
            }
        }

        /// <summary>
        /// Initializes the collection of sources.
        /// </summary>
        private void InitializeSources()
        {
            SourceDAL dal = new SourceDAL();
            this.sources = dal.GetAllSources();
        }
    }
}