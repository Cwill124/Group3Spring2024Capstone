﻿namespace CapstoneASP.Model;

/// <summary>
///     Represents a Note entity with properties such as Note_Id, Source_Id, Content, and Username.
/// </summary>
public class Note
{
    #region Properties

    /// <summary>
    ///     Gets or sets the unique identifier for the note.
    /// </summary>
    public int Note_Id { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the source associated with the note.
    /// </summary>
    public int Source_Id { get; set; }

    /// <summary>
    ///     Gets or sets the content of the note.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    ///     Gets or sets the username associated with the note.
    /// </summary>
    public string Username { get; set; }

    public string? Tags { get; set; }

    #endregion

    #region Methods

    /// <summary>
    ///     Determines whether the current object is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>True if the objects are equal; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var otherNote = (Note)obj;
        return this.Note_Id == otherNote.Note_Id;
    }

    /// <summary>
    ///     Serves as a hash function for the current object.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return this.Note_Id.GetHashCode();
    }

    #endregion
}