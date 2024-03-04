namespace CapstoneASP.Model
{
    /// <summary>
    /// Represents a tag associated with a note in the application.
    /// </summary>
    public class Tags
    {
        #region Data members

        /// <summary>
        /// Gets or sets the unique identifier of the tag.
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the associated note.
        /// </summary>
        public int Note { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the specified object is equal to the current tag.
        /// </summary>
        /// <param name="obj">The object to compare with the current tag.</param>
        /// <returns>True if the specified object is equal to the current tag; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherTag = (Tags)obj;

            return this.TagId == otherTag.TagId;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current tag.</returns>
        public override int GetHashCode()
        {
            return this.TagId.GetHashCode();
        }
        #endregion
    }
}
