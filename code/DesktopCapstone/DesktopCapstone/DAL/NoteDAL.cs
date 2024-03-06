using System.Collections.ObjectModel;
using System.Data;
using Dapper;
using DesktopCapstone.model;
using DesktopCapstone.util;
using Npgsql;

namespace DesktopCapstone.DAL;

/// <summary>
///     Data Access Layer for handling operations related to Note entities.
/// </summary>
public class NoteDAL
{
    #region Data members

    private readonly IDbConnection dbConnection;

    #endregion

    #region Constructors

    public NoteDAL(IDbConnection connection)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        this.dbConnection = connection;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Retrieves a collection of notes by their associated ID.
    /// </summary>
    /// <param name="id">The ID of the notes to retrieve.</param>
    /// <returns>An ObservableCollection of Note objects.</returns>
    public ObservableCollection<Note> GetNoteById(int id)
    {
        //connection.Open();

        var notes = new ObservableCollection<Note>();

        this.dbConnection.Open();

        var result = this.dbConnection.Query<Note>(SqlConstants.GetNotesById, new { id });

        foreach (var item in result.ToList())
        {
            item.TagList = new ObservableCollection<Tags>();
            notes.Add(item);
        }

        this.AddTagsToNotes(notes);
        this.dbConnection.Close();
        return notes;
    }

    /// <summary>
    ///     Creates a new note with the provided information.
    /// </summary>
    /// <param name="newNote">The Note object containing note details.</param>
    /// <returns>True if the note creation is successful; otherwise, false.</returns>
    public Note CreateNote(Note newNote)
    {
        this.dbConnection.Open();
        var note = this.dbConnection.QueryFirstOrDefault<Note>(SqlConstants.CreateNewNote, newNote);
        this.dbConnection.Close();

        return note;
    }

    /// <summary>
    ///     Deletes a note by its ID.
    /// </summary>
    /// <param name="id">The ID of the note to be deleted.</param>
    public int DeleteNoteById(int id)
    {
        var connection = new NpgsqlConnection(Connection.ConnectionString);

        connection.Open();

        var result = connection.Execute(SqlConstants.DeleteNoteById, new { id });

        connection.Close();
        return result;
    }

    /// <summary>
    ///     Searches for notes by name and username, returning a collection of matching notes.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <param name="username">The username associated with the notes.</param>
    /// <returns>An ObservableCollection of matching Note objects.</returns>
    public ObservableCollection<Note> SearchNotesByName(string name, string username)
    {
        //var connection = new NpgsqlConnection(Connection.ConnectionString);
        var notes = new ObservableCollection<Note>();
        this.dbConnection.Open();

        var result = this.dbConnection.Query<Note>(SqlConstants.GetNotesByName, new { name, username });
        var resultContains = this.dbConnection.Query<Note>(SqlConstants.GetNotesByNameContains, new { name, username });

        foreach (var item in result.ToList())
        {
            item.TagList = new ObservableCollection<Tags>();
            notes.Add(item);
        }

        foreach (var item in resultContains.ToList())
        {
            item.TagList = new ObservableCollection<Tags>();
            if (!notes.Contains(item))
            {
                notes.Add(item);
            }
        }

        this.AddTagsToNotes(notes);

        this.dbConnection.Close();

        return notes;
    }

    /// <summary>
    ///     Updates the content of a given note.
    /// </summary>
    /// <param name="note">The Note object containing updated content.</param>
    public int UpdateNoteContent(Note note)
    {
        this.dbConnection.Open();
        var result = this.dbConnection.Execute(SqlConstants.UpdateNoteContent, note);
        this.dbConnection.Close();
        return result;
    }

    /// <summary>
    ///     Retrieves all notes associated with a given username.
    /// </summary>
    /// <param name="username">The username to retrieve notes for.</param>
    /// <returns>An ObservableCollection of Note objects.</returns>
    public ObservableCollection<Note> GetAllNotesFromUser(string username)
    {
        var notes = new ObservableCollection<Note>();
        this.dbConnection.Open();

        var result = this.dbConnection.Query<Note>(SqlConstants.GetNotesByUsername, new { username });

        foreach (var item in result.ToList())
        {
            item.TagList = new ObservableCollection<Tags>();
            notes.Add(item);
        }

        this.AddTagsToNotes(notes);

        this.dbConnection.Close();
        return notes;
    }

    private void AddTagsToNotes(ObservableCollection<Note> notes)
    {
        foreach (var note in notes)
        {
            var result = this.dbConnection.Query<Tags>(SqlConstants.GetTagsByNoteId, note);

            foreach (var item in result.ToList())
            {
                note.TagList.Add(item);
            }
        }
    }

    #endregion
}