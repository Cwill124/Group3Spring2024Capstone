using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using desktop_capstone.DAL;
using DesktopCapstone.DAL;
using DesktopCapstone.model;

namespace DesktopCapstone.viewmodel
{
    public class NoteViewerViewModel
    {
        private NoteDAL noteDal;
        private string username;
        public ObservableCollection<Note> Notes { get; set; }
        public ObservableCollection<Tag> FilteredTags { get; set; }

        public NoteViewerViewModel(NoteDAL noteDal, string username)
        {
            this.noteDal = noteDal;
            this.Notes = new ObservableCollection<Note>();
            this.FilteredTags = new ObservableCollection<Tag>();
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
            var notes = this.noteDal.SearchNotesByName(name, username);
            foreach (var note in notes)
            {
                this.Notes.Add(note);
            }
        }

        public ObservableCollection<Tag> GetAllTagsFromNotes()
        {
            var tags = new ObservableCollection<Tag>();
            foreach (var note in this.Notes)
            {
                foreach (var tag in note.Tags)
                {
                    if (!tags.Any(t=> t.TagName == tag.TagName))
                    {
                        tags.Add(tag);
                    }
                }
            }
            return tags;
        }

        public void FilterNotesByTag()
        {
            var filteredNotes = new HashSet<Note>();
            foreach (var tag in this.FilteredTags)
            {
                foreach (var note in this.Notes)
                {
                    if (note.HasTag(tag))
                    {
                        filteredNotes.Add(note);
                    }
                }
            }
            this.Notes.Clear();
            foreach (var note in filteredNotes)
            {
                this.Notes.Add(note);
            }
        }

        public void RemoveTagFromFilter(Tag givenTag)
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

    }
}
