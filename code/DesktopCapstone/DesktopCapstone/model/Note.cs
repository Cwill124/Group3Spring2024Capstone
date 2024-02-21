using Newtonsoft.Json.Linq;

namespace DesktopCapstone.model
{
    /// <summary>
    /// Represents a note with associated information such as source ID, note ID, content, and username.
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Gets or sets the source ID associated with the note.
        /// </summary>
        public int SourceId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the note.
        /// </summary>
        public int NoteId { get; set; }

        /// <summary>
        /// Gets or sets the content of the note, typically in JSON format.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the username associated with the note.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Converts the note's content to a string representation, extracting the note title and content.
        /// </summary>
        /// <returns>A string representation of the note, including its title and content.</returns>
        public override string ToString()
        {
            var json = JObject.Parse(Content);

            // Extracting note title and content from the JSON
            return (string)json["noteTitle"] + "\n\n"  + (string)json["noteContent"];
        }
    }
}