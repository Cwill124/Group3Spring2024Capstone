namespace DesktopCapstone.model
{
    /// <summary>
    /// Represents a type of information source with attributes such as source type ID and type name.
    /// </summary>
    public class SourceType
    {
        /// <summary>
        /// Gets or sets the unique identifier for the source type.
        /// </summary>
        public int SourceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the source type.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Converts the source type to a string representation, returning the source type name.
        /// </summary>
        /// <returns>A string representation of the source type, which is its name.</returns>
        public override string ToString()
        {
            return this.TypeName;
        }
    }
}