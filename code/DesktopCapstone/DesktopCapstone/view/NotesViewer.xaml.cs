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
using System.Windows.Navigation;
using System.Windows.Shapes;
using desktop_capstone.DAL;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using DesktopCapstone.viewmodel;
using Npgsql;
using Button = System.Windows.Controls.Button;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for NotesViewer.xaml
    /// </summary>
    public partial class NotesViewer : Window
    {

        private NoteViewerViewModel viewModel;
        private string username;
        /// <summary>
        /// Gets or sets the currently selected note.
        /// </summary>
        public Note? CurrentNote { get; private set; }
        public Tags? CurrentTag { get; private set; }

        public NotesViewer(string username)
        {
            InitializeComponent();
            this.username = username;
            NoteDAL dal = new NoteDAL(new NpgsqlConnection(Connection.ConnectionString));
            this.viewModel = new NoteViewerViewModel(dal, username);
            this.DataContext = this.viewModel;
            //this.lstNotes.ItemsSource = this.viewModel.Notes;
            this.viewModel.RefreshNotes();
            //this.lstTags.ItemsSource = this.viewModel.FilteredTags;
            Debug.WriteLine(this.viewModel.Notes.Count);

        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            Main newPage = new Main(this.username);
            newPage.Show();
            this.Close();
        }

        /// <summary>
        /// Event handler for the mouse enter event on UI elements (buttons).
        /// Changes the background and foreground colors to indicate interaction.
        /// </summary>
        private void UIElement_OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
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
        private void UIElement_OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button)
            {
                button.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 255, 255, 255));
                button.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 52, 152, 219));
            }
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.SearchNotesByName(this.txtSearch.Text);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.RefreshNotes();
        }

        private void BtnFilter_OnClickClick(object sender, RoutedEventArgs e)
        {
            var tags = this.viewModel.GetAllTagsFromNotes();
            if (tags.Count == 0)
            {
                System.Windows.MessageBox.Show("No tags found.");
                return;
            }

            TagFilterWindow tagFilterWindow = new TagFilterWindow(tags);
            tagFilterWindow.ShowDialog();
            var selectedTag = tagFilterWindow.SelectedTag;
            this.viewModel.FilteredTags.Add(selectedTag);
            this.viewModel.FilterNotesByTag();
        }

        private void btnDeleteFilterTag_Click(object sender, RoutedEventArgs e)
        {
            //string text = textBlock.Text;

            if (this.CurrentTag is not null)
            {
                this.viewModel.RemoveTagFromFilter(this.CurrentTag);
            }
        }

        /// <summary>
        /// Event handler for the selection change in the list of notes.
        /// Updates the current note based on the selection.
        /// </summary>
        private void lstNotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void lstTags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lstTagsBox = (System.Windows.Controls.ListBox)sender;

            if (lstTagsBox.SelectedItem != null)
            {
                this.CurrentTag = (Tags?)lstTagsBox.SelectedItem;
            }
            else
            {
                // Handle the case where nothing is selected, if needed
                this.CurrentTag = null;
            }
        }

        private void btnOpenExpandNote(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            StackPanel stackPanel = (StackPanel)button.Parent;

            var note = (Note)stackPanel.DataContext;

            var tagExpand = new ExpandedNote(note);

            tagExpand.ShowDialog();

            this.viewModel.RefreshNotes();
        }
    }
}
