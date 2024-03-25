namespace CapstoneASP.Model
{
    /// <summary>
    /// Represents a project entity.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the ID of the project.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the title of the project.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the project.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the owner of the project.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherProject = (Project)obj;

            return this.ProjectId == otherProject.ProjectId;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current project.</returns>
        public override int GetHashCode()
        {
            return this.ProjectId.GetHashCode();
        }
    }
}