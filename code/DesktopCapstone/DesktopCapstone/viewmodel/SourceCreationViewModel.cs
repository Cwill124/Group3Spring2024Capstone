using DesktopCapstone.model;
using System.Collections.ObjectModel;
using DesktopCapstone.DAL;

namespace DesktopCapstone.viewmodel
{
    /// <summary>
    /// View model for the SourceCreation window, providing data for source type and format selection.
    /// </summary>
    public class SourceCreationViewModel
    {
        private ObservableCollection<SourceType> sourceTypes;
        private ObservableCollection<string> sourceFormat;

        /// <summary>
        /// Gets the collection of source types.
        /// </summary>
        public ObservableCollection<SourceType> SourceTypes { get { return this.sourceTypes; } }

        /// <summary>
        /// Gets the collection of source formats.
        /// </summary>
        public ObservableCollection<string> SourceFormat { get { return this.sourceFormat; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceCreationViewModel"/> class with default values.
        /// </summary>
        public SourceCreationViewModel()
        {
            this.sourceTypes = new ObservableCollection<SourceType>();
            this.sourceFormat = new ObservableCollection<string>();
            this.InitializeLists();
        }

        /// <summary>
        /// Initializes the collections of source types and formats.
        /// </summary>
        private void InitializeLists()
        {
            sourceTypes = DALConnection.SourceDAL.GetSourceTypes();

            // Initializing source formats with "URL" and "File"
            sourceFormat = new ObservableCollection<string>(new List<string> { "URL", "File" });
        }
    }
}