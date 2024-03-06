namespace DesktopCapstone.model;

/// <summary>
///     Represents a source of information with various attributes such as source ID, description, name, content, metadata,
///     source type, tags, and creator information.
/// </summary>
public class Source
{
    #region Properties

    /// <summary>
    ///     Gets or sets the unique identifier for the source.
    /// </summary>
    public int? SourceId { get; set; }

    /// <summary>
    ///     Gets or sets the description of the source.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Gets or sets the name of the source.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets the content associated with the source.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Gets or sets the metadata related to the source.
    /// </summary>
    public string MetaData { get; set; }

    /// <summary>
    ///     Gets or sets the type of the source.
    /// </summary>
    public int SourceType { get; set; }

    /// <summary>
    ///     Gets or sets the tags associated with the source.
    /// </summary>
    public string Tags { get; set; }

    /// <summary>
    ///     Gets or sets the creator information of the source.
    /// </summary>
    public string CreatedBy { get; set; }

    #endregion

    #region Methods

    /// <summary>
    ///     Converts the source to a string representation, returning the source name.
    /// </summary>
    /// <returns>A string representation of the source, which is its name.</returns>
    public override string ToString()
    {
        return this.Name;
    }

    #endregion
}