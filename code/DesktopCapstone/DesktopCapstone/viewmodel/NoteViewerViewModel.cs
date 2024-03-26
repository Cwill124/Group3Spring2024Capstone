using System.Collections.ObjectModel;
using System.Diagnostics;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Npgsql;

namespace DesktopCapstone.viewmodel;

/// <summary>
///     ViewModel for managing notes and tags in the application's note viewer.
/// </summary>
public class NoteViewerViewModel
{
    #region Data members

    private readonly NoteDAL noteDal;
    private readonly string username;

    #endregion

    #region Properties

    /// <summary>
    ///     Gets or sets the collection of notes to be displayed in the viewer.
    /// </summary>
    public ObservableCollection<Note> Notes { get; set; }

    /// <summary>
    ///     Gets or sets the collection of filtered tags to be applied to notes.
    /// </summary>
    public ObservableCollection<Tags> FilteredTags { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the NoteViewerViewModel class.
    /// </summary>
    /// <param name="noteDal">The data access layer for managing notes.</param>
    /// <param name="username">The username associated with the current user.</param>
    public NoteViewerViewModel(NoteDAL noteDal, string username)
    {
        this.noteDal = noteDal;
        this.Notes = new ObservableCollection<Note>();
        this.FilteredTags = new ObservableCollection<Tags>();
        this.username = username;
        //this.RefreshNotes();
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Refreshes the list of notes displayed in the viewer.
    /// </summary>
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

    /// <summary>
    ///     Searches for notes by name and updates the displayed notes accordingly.
    /// </summary>
    /// <param name="name">The name to search for in note titles.</param>
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

    /// <summary>
    ///     Retrieves all tags associated with the user's notes.
    /// </summary>
    /// <returns>The collection of tags belonging to the user.</returns>
    public ObservableCollection<Tags> GetAllTagsFromNotes(TagDAL dal)
    {
        var tags = new ObservableCollection<Tags>();
        //var tagDal = new TagDAL(new NpgsqlConnection(Connection.ConnectionString));
        tags = dal.GetTagsBelongingToUser(this.username);
        return tags;
    }

    /// <summary>
    ///     Filters notes based on the selected tags.
    /// </summary>
    public void FilterNotesByTag()
    {
        this.GetBaseNotes();
        var noteMatchingTagsCount = new Dictionary<Note, int>();

        foreach (var note in this.Notes)
        {
            var matchingTagsCount = this.FilteredTags.Count(tag => note.HasTag(tag));
            if (matchingTagsCount > 0) // Only consider notes with matching tags
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

    /// <summary>
    ///     Removes a tag from the filter and updates the displayed notes accordingly.
    /// </summary>
    /// <param name="givenTag">The tag to be removed from the filter.</param>
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

    #endregion
}