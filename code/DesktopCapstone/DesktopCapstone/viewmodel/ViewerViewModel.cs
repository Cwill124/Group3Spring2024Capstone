using desktop_capstone.DAL;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace DesktopCapstone.viewmodel
{
    /// <summary>
    /// View model for the PDFViewer and VideoViewer window, providing data for source and note management.
    /// </summary>
    public class ViewerViewModel
    {
        private ObservableCollection<Source> sources;
        private ObservableCollection<Note> notes;
        private int currentSourceId;

        /// <summary>
        /// Gets the collection of sources.
        /// </summary>
        public ObservableCollection<Source> Sources { get { return this.sources; } }

        /// <summary>
        /// Gets the collection of notes.
        /// </summary>
        public ObservableCollection<Note> Notes { get { return this.notes; } }
        /// <summary>
        /// Gets the current source link.
        /// </summary>
        public Uri CurrentSourceLink { get; set; }

        /// <summary>
        /// Gets or sets the current source ID.
        /// When set, initializes the source link and refreshes notes for the new source.
        /// </summary>
        public int CurrentSourceId
        {
            get { return this.currentSourceId; }
            set { this.currentSourceId = value; this.initializeSourceLink(); this.RefreshNotes(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerViewModel"/> class with the specified current source ID.
        /// </summary>
        /// <param name="currentSourceId">The ID of the current source.</param>
        public ViewerViewModel(int currentSourceId)
        {
            this.sources = new ObservableCollection<Source>();
            this.notes = new ObservableCollection<Note>();
            this.initializeLists();
            this.CurrentSourceId = currentSourceId;
            this.initializeSourceLink();
        }

        /// <summary>
        /// Refreshes the collection of sources from the data source.
        /// </summary>
        public void RefreshSources()
        {
            this.sources.Clear();
            //SourceDAL dal = new SourceDAL();
            foreach (Source source in DALConnection.SourceDAL.GetAllSources())
            {
                this.sources.Add(source);
            }
        }

        /// <summary>
        /// Refreshes the collection of notes for the current source from the data source.
        /// </summary>
        public void RefreshNotes()
        {
            this.notes.Clear();
            //NoteDAL dal = new NoteDAL();
            foreach (Note note in DALConnection.NoteDAL.GetNoteById(this.currentSourceId))
            {
                this.notes.Add(note);
            }
        }

        private void initializeLists()
        {

            this.sources = DALConnection.SourceDAL.GetAllSources();
            this.notes = DALConnection.NoteDAL.GetNoteById(this.currentSourceId);
        }

        private void initializeSourceLink()
        {
            //SourceDAL sourceDal = new SourceDAL();
            var source = DALConnection.SourceDAL.GetSourceWithId(this.currentSourceId);
            var json = JObject.Parse(source.Content);
            var link = (string)json["url"];
            this.CurrentSourceLink = new Uri(link);

        }
    }
}
