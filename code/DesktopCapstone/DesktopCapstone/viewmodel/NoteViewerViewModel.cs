using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using desktop_capstone.DAL;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Npgsql;

namespace DesktopCapstone.viewmodel
{
    public class NoteViewerViewModel
    {
        private NoteDAL noteDal;
        private string username;
        public ObservableCollection<Note> Notes { get; set; }
        public ObservableCollection<Tags> FilteredTags { get; set; }

        public NoteViewerViewModel(NoteDAL noteDal, string username)
        {
            this.noteDal = noteDal;
            this.Notes = new ObservableCollection<Note>();
            this.FilteredTags = new ObservableCollection<Tags>();
            this.username = username;
            //this.RefreshNotes();
        }

        public void RefreshNotes()
        {
            this.Notes.Clear();
            this.FilteredTags.Clear();
            var notes = this.noteDal.GetAllNotesFromUser(this.username);
            foreach (var note in notes)
            {
                this.Notes.Add(note);
            }
        }

        public void SearchNotesByName(string name)
        {
            this.Notes.Clear();
            var notes = this.noteDal.SearchNotesByName(name, this.username);
            Debug.WriteLine("notes with name: " + notes.Count);
            foreach (var note in notes)
            {
                this.Notes.Add(note);
            }
        }

        public ObservableCollection<Tags> GetAllTagsFromNotes()
        {
            var tags = new ObservableCollection<Tags>();
            TagDAL tagDal = new TagDAL(new NpgsqlConnection(Connection.ConnectionString));
            tags = tagDal.GetTagsBelongingToUser(this.username);
            
            return tags;
        }

        public void FilterNotesByTag()
        {
            this.GetBaseNotes();
            var noteMatchingTagsCount = new Dictionary<Note, int>();

            foreach (var note in this.Notes)
            {
                int matchingTagsCount = this.FilteredTags.Count(tag => note.HasTag(tag));
                if (matchingTagsCount > 0)  // Only consider notes with matching tags
                {
                    noteMatchingTagsCount.Add(note, matchingTagsCount);
                }
            }
            var sortedNotes = noteMatchingTagsCount.OrderByDescending(pair => pair.Value).Select(pair => pair.Key).ToList();

            this.Notes.Clear();
            foreach (var note in sortedNotes)
            {
                this.Notes.Add(note);
            }
        }

        public void RemoveTagFromFilter(Tags givenTag)
        {
            this.FilteredTags.Remove(givenTag);
            if (this.FilteredTags.Count == 0)
            {
                this.RefreshNotes();
            }
            else
            {
                this.FilterNotesByTag();
            }
        }

        private void GetBaseNotes()
        {
            this.Notes.Clear();
            var notes = this.noteDal.GetAllNotesFromUser(this.username);
            foreach (var note in notes)
            {
                this.Notes.Add(note);
            }
        }

    }
}
