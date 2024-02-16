namespace CapstoneASP.Model;

/// <summary>
///     Represents a Source entity with properties such as SourceId, Name, Description, Content, MetaData, Tags,
///     SourceTypeId, and CreatedBy.
/// </summary>
public class Source
{
    #region Properties

    /// <summary>
    ///     Gets or sets the unique identifier for the source.
    /// </summary>
    public int SourceId { get; set; }

    /// <summary>
    ///     Gets or sets the name of the source.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets the description of the source.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Gets or sets the content of the source.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Gets or sets the metadata associated with the source.
    /// </summary>
    public string MetaData { get; set; }

    /// <summary>
    ///     Gets or sets the tags associated with the source.
    /// </summary>
    public string Tags { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the source type.
    /// </summary>
    public int SourceTypeId { get; set; }

    /// <summary>
    ///     Gets or sets the username of the creator of the source.
    /// </summary>
    public string CreatedBy { get; set; }

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

        var otherSource = (Source)obj;
        return this.SourceId == otherSource.SourceId;
    }

    /// <summary>
    ///     Serves as a hash function for the current object.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return this.SourceId.GetHashCode();
    }

    #endregion
}