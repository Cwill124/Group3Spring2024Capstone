using DesktopCapstone.viewmodel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for PDFViewer.xaml
    /// </summary>
    public partial class PDFViewer : Window
    {
        private int currentSourceId;
        private string username;
        private PDFViewerViewModel viewModel;

        /// <summary>
        /// Gets or sets the currently selected note.
        /// </summary>
        public Note? CurrentNote { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFViewer"/> class with default values.
        /// </summary>
        public PDFViewer()
        {
            InitializeComponent();
            this.viewModel = new PDFViewerViewModel(this.currentSourceId);
            this.DataContext = viewModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFViewer"/> class with specified parameters.
        /// </summary>
        /// <param name="currentSourceId">The ID of the current source.</param>
        /// <param name="username">The username associated with the viewer.</param>
        public PDFViewer(int currentSourceId, string username)
        {
            InitializeComponent();
            this.currentSourceId = currentSourceId;
            this.username = username;
            this.viewModel = new PDFViewerViewModel(this.currentSourceId);
            this.DataContext = viewModel;
            this.lstSources.ItemsSource = this.viewModel.Sources;
            this.lstNotes.ItemsSource = this.viewModel.Notes;
            this.webPDF.Source = this.viewModel.CurrentSourceLink;
        }

        /// <summary>
        /// Event handler for the "Add Source" button click.
        /// Opens a dialog for creating a new source and refreshes the list of sources.
        /// </summary>
        private void btnAddSource_Click(object sender, RoutedEventArgs e)
        {
            SourceCreation sourceCreationDialog = new SourceCreation();
            sourceCreationDialog.ShowDialog();
            this.viewModel.RefreshSources();
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
        /// Event handler for the selection change in the list of sources.
        /// Updates the current source ID and displays the corresponding PDF content.
        /// </summary>
        private void lstSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var source = (Source)this.lstSources.SelectedItem;
            this.viewModel.CurrentSourceId = (int)source.SourceId;
            this.webPDF.Source = this.viewModel.CurrentSourceLink;
        }

        /// <summary>
        /// Event handler for the "Return" button click.
        /// Closes the current window and opens the SourcesViewer window.
        /// </summary>
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
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
            DALConnection.SourceDAL.DeleteById(this.currentSourceId);
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
    }
}
