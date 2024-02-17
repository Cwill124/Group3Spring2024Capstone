using desktop_capstone.DAL;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace DesktopCapstone.viewmodel
{
    /// <summary>
    /// View model for the PDFViewer window, providing data for source and note management.
    /// </summary>
    public class PDFViewerViewModel
    {
        private ObservableCollection<Source> sources;
        private ObservableCollection<Note> notes;
        private Uri currentSourceLink;
        private int currentSourceId;

        /// <summary>
        /// Gets the collection of sources.
        /// </summary>
        public ObservableCollection<Source> Sources { get { return sources; } }

        /// <summary>
        /// Gets the collection of notes.
        /// </summary>
        public ObservableCollection<Note> Notes { get { return notes; } }

        /// <summary>
        /// Gets the current source link.
        /// </summary>
        public Uri CurrentSourceLink { get { return currentSourceLink; } }

        /// <summary>
        /// Gets or sets the current source ID.
        /// When set, initializes the source link and refreshes notes for the new source.
        /// </summary>
        public int CurrentSourceId
        {
            get { return currentSourceId; }
            set { this.currentSourceId = value; this.InitializeSourceLink(); this.RefreshNotes(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFViewerViewModel"/> class with the specified current source ID.
        /// </summary>
        /// <param name="currentSourceId">The ID of the current source.</param>
        public PDFViewerViewModel(int currentSourceId)
        {
            this.sources = new ObservableCollection<Source>();
            this.notes = new ObservableCollection<Note>();
            this.InitializeLists();
            CurrentSourceId = currentSourceId;
            this.InitializeSourceLink();
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
        /// Refreshes the collection of notes for the current source from the data source.
        /// </summary>
        public void RefreshNotes()
        {
            this.notes.Clear();
            NoteDAL dal = new NoteDAL();
            foreach (Note note in dal.GetNoteById(this.currentSourceId))
            {
                this.notes.Add(note);
            }
        }

        private void InitializeLists()
        {
            SourceDAL sourceDal = new SourceDAL();
            NoteDAL noteDal = new NoteDAL();

            sources = sourceDal.GetAllSources();
            notes = noteDal.GetNoteById(this.currentSourceId);
        }

        private void InitializeSourceLink()
        {
            SourceDAL sourceDal = new SourceDAL();
            var source = sourceDal.GetSourceWithId(this.currentSourceId);
            var json = JObject.Parse(source.Content);
            var link = (string)json["url"];
            this.currentSourceLink = new Uri(link);
        }
    }
}
