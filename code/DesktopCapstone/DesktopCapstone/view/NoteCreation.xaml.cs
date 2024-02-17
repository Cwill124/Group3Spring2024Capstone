using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Newtonsoft.Json;
using System.Windows;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for NoteCreation.xaml
    /// </summary>
    public partial class NoteCreation : Window
    {
        private int currentSourceId;
        private string username;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteCreation"/> class with default values.
        /// </summary>
        public NoteCreation()
        {
            InitializeComponent();
            this.currentSourceId = 0;
            this.username = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteCreation"/> class with specified parameters.
        /// </summary>
        /// <param name="currentSourceId">The ID of the current source.</param>
        /// <param name="username">The username associated with the note.</param>
        public NoteCreation(int currentSourceId, string username)
        {
            InitializeComponent();
            this.currentSourceId = currentSourceId;
            this.username = username;
        }

        /// <summary>
        /// Event handler for the "Create" button click.
        /// Creates a new note based on user input and adds it to the database.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var title = this.txtTitle.Text;
            var textContent = this.txtContent.Text;
            var content = JsonConvert.SerializeObject(new { noteTitle = title, noteContent = textContent });

            var noteToAdd = new Note
            {
                Content = content,
                SourceId = this.currentSourceId,
                Username = this.username
            };

            NoteDAL dal = new NoteDAL();
            dal.CreateNote(noteToAdd);
            this.Close();
        }
    }
}
