using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace DesktopCapstone.model;

/// <summary>
///     Represents a note with associated information such as source ID, note ID, content, and username.
/// </summary>
public class Note : IEquatable<Note>
{
    #region Properties

    /// <summary>
    ///     Gets or sets the source ID associated with the note.
    /// </summary>
    public int SourceId { get; set; }

    /// <summary>
    ///     Gets or sets the unique identifier for the note.
    /// </summary>
    public int NoteId { get; set; }

    /// <summary>
    ///     Gets or sets the content of the note, typically in JSON format.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Gets or sets the username associated with the note.
    /// </summary>
    public string Username { get; set; }

    public ObservableCollection<Tags> TagList { get; set; }

    #endregion

    #region Methods

    /// <summary>
    ///     Determines whether the current note is equal to another note.
    /// </summary>
    /// <param name="other">The note to compare with the current note.</param>
    /// <returns>true if the notes are equal; otherwise, false.</returns>
    public bool Equals(Note other)
    {
        return this.NoteId == other.NoteId;
    }

    /// <summary>
    ///     Converts the note's content to a string representation, extracting the note title and content.
    /// </summary>
    /// <returns>A string representation of the note, including its title and content.</returns>
    public override string ToString()
    {
        var json = JObject.Parse(this.Content);

        // Extracting note title and content from the JSON
        return (string)json["note_Title"] + "\n\n" + (string)json["note_Content"];
    }

    /// <summary>
    ///     Gets the title of the note from the JSON content.
    /// </summary>
    /// <returns>The title of the note.</returns>
    public string GetTitle()
    {
        var json = JObject.Parse(this.Content);
        return (string)json["note_Title"];
    }

    /// <summary>
    ///     Gets the content of the note from the JSON content.
    /// </summary>
    /// <returns>The content of the note.</returns>
    public string GetContent()
    {
        var json = JObject.Parse(this.Content);
        return (string)json["note_Content"];
    }

    /// <summary>
    ///     Checks if the note has a specific tag.
    /// </summary>
    /// <param name="tag">The tag to check for in the note's tag list.</param>
    /// <returns>true if the note has the specified tag; otherwise, false.</returns>
    public bool HasTag(Tags tag)
    {
        if (tag != null)
        {
            foreach (var current in this.TagList)
            {
                if (current.Tag == tag.Tag)
                {
                    return true;
                }
            }
        }

        return false;
    }

    #endregion
}