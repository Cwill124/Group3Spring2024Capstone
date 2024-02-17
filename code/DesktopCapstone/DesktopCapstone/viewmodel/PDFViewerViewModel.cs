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
        private Uri currentSourceLink;
        private int currentSourceId;
        
        public ObservableCollection<Source> Sources { get { return sources; } }
        public ObservableCollection<Note> Notes { get { return notes; } }
        public Uri CurrentSourceLink { get { return currentSourceLink; } }
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
            foreach (Source source in dal.GetAllSources())
            {
                this.sources.Add(source);
            }

        }

        public void refreshNotes()
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

        private void initializeSourceLink()
        {
            SourceDAL sourceDal = new SourceDAL();
            var source = sourceDal.GetSourceWithId(this.currentSourceId);
            var json = JObject.Parse(source.Content);
            var link = (string)json["url"];
            this.currentSourceLink = new Uri(link);

        }
    }
}
