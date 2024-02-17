using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using DesktopCapstone.viewmodel;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for VideoViewer.xaml
    /// </summary>
    public partial class VideoViewer : Window
    {
        private int currentSourceId;
        private string username;
        private ViewerViewModel viewModel;

        /// <summary>
        /// Gets or sets the currently selected note.
        /// </summary>
        public Note? CurrentNote { get; private set; }

        public VideoViewer(int currentSourceId, string username)
        {
            InitializeComponent();
            this.currentSourceId = currentSourceId;
            this.username = username;
            this.viewModel = new ViewerViewModel(this.currentSourceId);
            this.DataContext = viewModel;
            this.lstSources.ItemsSource = this.viewModel.Sources;
            this.lstNotes.ItemsSource = this.viewModel.Notes;
            this.loadVideo();
        }

        private void btnAddSource_Click(object sender, RoutedEventArgs e)
        {
            SourceCreation sourceCreationDialog = new SourceCreation();
            sourceCreationDialog.ShowDialog();
            this.viewModel.RefreshSources();
        }

        private void btnAddNotes_Click(object sender, RoutedEventArgs e)
        {
            NoteCreation noteCreationDialog = new NoteCreation(this.viewModel.CurrentSourceId, this.username);
            noteCreationDialog.ShowDialog();
            this.viewModel.RefreshNotes();
        }

        private void lstSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var source = (Source)this.lstSources.SelectedItem;

            if ((int)source.SourceType == 1)
            {
                this.viewModel.CurrentSourceId = (int)source.SourceId;
                this.switchToPDFViewer();
            }
            else
            {
                this.viewModel.CurrentSourceId = (int)source.SourceId;
                this.loadVideo();
            }
        }   

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            SourcesViewer viewer = new SourcesViewer(this.username);
            viewer.Show();
            this.Close();
        }

        private void switchToPDFViewer()
        {
            PDFViewer viewer = new PDFViewer(this.viewModel.CurrentSourceId, this.username);
            viewer.Show();
            this.Close();
        }
        

        private void loadVideo()
        {
            if (this.viewModel.CurrentSourceLink.ToString().Contains("youtube"))
            {
                var youtubeLink = this.viewModel.CurrentSourceLink.ToString();
                var youtubeId = this.convertYoutubeLinkToId(youtubeLink);
                this.loadEmbeddedYoutubeVideo(youtubeId);
            }
            else
            {
                this.webVideo.Source = this.viewModel.CurrentSourceLink;
            }
        }

        private void loadEmbeddedYoutubeVideo(string youtubeId)
        {
            var youtubeLink = "https://www.youtube.com/embed/" + youtubeId;
            this.viewModel.CurrentSourceLink = new Uri(youtubeLink);
            this.webVideo.Source = this.viewModel.CurrentSourceLink;
        }

        private string convertYoutubeLinkToId(string youtubeLink)
        {
            var id = youtubeLink.Substring(youtubeLink.IndexOf('=') + 1);
            return id;
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

        /// <summary>
        /// Event handler for the "Delete Source" button click.
        /// Deletes the current source and returns to the SourcesViewer window.
        /// </summary>
        private void btnDeleteSource_Click(object sender, RoutedEventArgs e)
        {
            DALConnection.SourceDAL.DeleteById(this.currentSourceId);
            var viewer = new SourcesViewer(this.username);
            viewer.Show();
            this.Close();
        }
    }
}