using DesktopCapstone.viewmodel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Button = System.Windows.Controls.Button;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for Viewer.xaml
    /// </summary>
    public partial class Viewer : Window
    {
        private string username;
        private ViewerViewModel viewModel;
        private int sourceType;

        /// <summary>
        /// Gets or sets the currently selected note.
        /// </summary>
        public Note? CurrentNote { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Viewer"/> class with default values.
        /// </summary>
        public Viewer()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Viewer"/> class with specified parameters.
        /// </summary>
        /// <param name="currentSourceId">The ID of the current source.</param>
        /// <param name="username">The username associated with the viewer.</param>
        public Viewer(int currentSourceId, string username, int sourceType)
        {
            InitializeComponent();
            this.username = username;
            this.sourceType = sourceType;
            this.viewModel = new ViewerViewModel(currentSourceId, this.sourceType);
            this.DataContext = viewModel;
            this.lstNotes.ItemsSource = this.viewModel.Notes;
            this.webPDF.Source = this.viewModel.CurrentSourceLink;
            
        }


        /// <summary>
        /// Event handler for the "Add Notes" button click.
        /// Opens a dialog for creating a new note and refreshes the list of notes.
        /// </summary>
        private void btnAddNotes_Click(object sender, RoutedEventArgs e)
        {
            NoteCreation noteCreationDialog = new NoteCreation(this.viewModel.CurrentSourceId, this.username);
            noteCreationDialog.ShowDialog();
            this.viewModel.RefreshNotes();
        }


        /// <summary>
        /// Event handler for the "Return" button click.
        /// Closes the current window and opens the SourcesViewer window.
        /// </summary>
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.webPDF.Stop();
            this.webPDF.CoreWebView2.Reload();
            SourcesViewer viewer = new SourcesViewer(this.username);
            viewer.Show();
            this.Close();
        }

        /// <summary>
        /// Event handler for the "Delete Source" button click.
        /// Deletes the current source and returns to the SourcesViewer window.
        /// </summary>
        private void btnDelete_Source(object sender, RoutedEventArgs e)
        {
            DALConnection.SourceDAL.DeleteById(this.viewModel.CurrentSourceId);
            var viewer = new SourcesViewer(this.username);
            viewer.Show();
            this.Close();
        }

        /// <summary>
        /// Event handler for the "Delete Note" button click.
        /// Deletes the current note and refreshes the list of notes.
        /// </summary>
        private void btnDelete_Note(object sender, RoutedEventArgs e)
        {
            if (this.CurrentNote is not null)
            {
                DALConnection.NoteDAL.DeleteNoteById(this.CurrentNote.NoteId);
                this.viewModel.RefreshNotes();
            }
        }

        /// <summary>
        /// Event handler for the selection change in the list of notes.
        /// Updates the current note based on the selection.
        /// </summary>
        private void LstNotes_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lstNotesBox = (System.Windows.Controls.ListBox)sender;

            if (lstNotesBox.SelectedItem != null)
            {
                this.CurrentNote = (Note?)lstNotesBox.SelectedItem;
            }
            else
            {
                // Handle the case where nothing is selected, if needed
                this.CurrentNote = null;
            }
        }

        /// <summary>
        /// Event handler for the mouse enter event on UI elements (buttons).
        /// Changes the background and foreground colors to indicate interaction.
        /// </summary>
        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button)
            {
                button.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 52, 152, 219));
                button.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 255, 255));
                button.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 255, 255, 255));
            }
        }

        /// <summary>
        /// Event handler for the mouse leave event on UI elements (buttons).
        /// Resets the background and foreground colors after mouse leave.
        /// </summary>
        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button)
            {
                button.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 255, 255, 255));
                button.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 52, 152, 219));
            }
        }

        private void btnDelete_Tag_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            StackPanel stackPanel = (StackPanel)button.Parent;
            TextBlock textBlock = (TextBlock)stackPanel.Children[0];

            var tag = (Tags)textBlock.DataContext;

            DALConnection.TagDal.DeleteTag(tag);
            this.viewModel.RefreshNotes();

        }
    }
}
