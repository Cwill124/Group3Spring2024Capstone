namespace CapstoneASP.Model
{
    /// <summary>
    /// Represents a model that contains a project identifier and a list of source identifiers.
    /// </summary>
    public class ProjectAndSources
    {
        /// <summary>
        /// Gets or sets the identifier of the project.
        /// </summary>
        /// <value>The identifier of the project.</value>
        public int projectId { get; set; }

        /// <summary>
        /// Gets or sets the list of source identifiers associated with the project.
        /// </summary>
        /// <value>The list of source identifiers.</value>
        public List<int> sources { get; set; }
    }
}