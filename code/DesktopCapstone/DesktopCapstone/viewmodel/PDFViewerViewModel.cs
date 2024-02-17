using desktop_capstone.DAL;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCapstone.viewmodel
{
    public class PDFViewerViewModel
    {

        private ObservableCollection<Source> sources;
        private ObservableCollection<Note> notes;
        //private Uri currentSourceLink;
        private int currentSourceId;
        
        public ObservableCollection<Source> Sources { get { return sources; } }
        public ObservableCollection<Note> Notes { get { return notes; } }
        public Uri CurrentSourceLink { get; set; }
        public int CurrentSourceId {  get { return currentSourceId; } set { this.currentSourceId = value; this.initializeSourceLink(); this.refreshNotes(); } }

        public PDFViewerViewModel(int currentSourceId)
        {
            this.sources = new ObservableCollection<Source>();
            this.notes = new ObservableCollection<Note>();
            this.InitializeLists();
            CurrentSourceId = currentSourceId;
            this.initializeSourceLink();

        }

        public void refreshSources()
        {
            this.sources.Clear();
            SourceDAL dal = new SourceDAL();
            foreach (Source source in dal.getAllSources())
            {
                this.sources.Add(source);
            }

        }

        public void refreshNotes()
        {
            this.notes.Clear();
            NoteDAL dal = new NoteDAL();
            foreach (Note note in dal.getNotesWithId(this.currentSourceId))
            {
                this.notes.Add(note);
            }

        }

        private void InitializeLists()
        {
            SourceDAL sourceDal = new SourceDAL();
            NoteDAL noteDal = new NoteDAL();

            sources = sourceDal.getAllSources();
            notes = noteDal.getNotesWithId(this.currentSourceId);
        }

        private void initializeSourceLink()
        {
            SourceDAL sourceDal = new SourceDAL();
            var source = sourceDal.getSourceWithId(this.currentSourceId);
            var json = JObject.Parse(source.Content);
            var link = (string)json["url"];
            this.CurrentSourceLink = new Uri(link);

        }
    }
}
