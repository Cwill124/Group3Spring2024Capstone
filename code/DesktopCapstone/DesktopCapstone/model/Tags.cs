namespace DesktopCapstone.model;

/// <summary>
///     Represents a tag associated with notes in the application.
/// </summary>
public class Tags
{
    #region Properties

    /// <summary>
    ///     Gets or sets the unique identifier for the tag.
    /// </summary>
    public int TagId { get; set; }

    /// <summary>
    ///     Gets or sets the name of the tag.
    /// </summary>
    public string Tag { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the associated note.
    /// </summary>
    public int Note { get; set; }

    #endregion

    #region Methods

    /// <summary>
    ///     Returns a string representation of the tag.
    /// </summary>
    /// <returns>The tag name as a string.</returns>
    public override string ToString()
    {
        return this.Tag;
    }

    #endregion
}