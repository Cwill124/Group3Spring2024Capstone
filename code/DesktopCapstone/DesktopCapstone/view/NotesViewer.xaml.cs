using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using DesktopCapstone.viewmodel;
using Npgsql;
using Button = System.Windows.Controls.Button;
using Color = System.Windows.Media.Color;
using ListBox = System.Windows.Controls.ListBox;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace DesktopCapstone.view;

/// <summary>
///     Interaction logic for NotesViewer.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class NotesViewer : Window
{
    #region Data members

    private readonly NoteViewerViewModel viewModel;
    private readonly string username;

    #endregion

    #region Properties

    /// <summary>
    ///     Gets or sets the currently selected note.
    /// </summary>
    public Note? CurrentNote { get; private set; }

    public Tags? CurrentTag { get; private set; }

    #endregion

    #region Constructors

    public NotesViewer(string username)
    {
        this.InitializeComponent();
        this.username = username;
        //var dal = new NoteDAL(new NpgsqlConnection(Connection.ConnectionString));
        this.viewModel = new NoteViewerViewModel(DALConnection.NoteDAL, username);
        DataContext = this.viewModel;
        this.viewModel.RefreshNotes();
        Debug.WriteLine(this.viewModel.Notes.Count);
    }

    #endregion

    #region Methods

    private void btnReturn_Click(object sender, RoutedEventArgs e)
    {
        var newPage = new Main(this.username);
        newPage.Show();
        Close();
    }

    /// <summary>
    ///     Event handler for the mouse enter event on UI elements (buttons).
    ///     Changes the background and foreground colors to indicate interaction.
    /// </summary>
    private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is Button button)
        {
            button.Background = new SolidColorBrush(Color.FromArgb(255, 52, 152, 219));
            button.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            button.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
        }
    }

    /// <summary>
    ///     Event handler for the mouse leave event on UI elements (buttons).
    ///     Resets the background and foreground colors after mouse leave.
    /// </summary>
    private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
    {
        if (sender is Button button)
        {
            button.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            button.Foreground = new SolidColorBrush(Color.FromArgb(255, 52, 152, 219));
        }
    }

    /// <summary>
    ///     Event handler for the "Delete Note" button click.
    ///     Deletes the current note and refreshes the list of notes.
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
        var tags = this.viewModel.GetAllTagsFromNotes(new TagDAL(new NpgsqlConnection(Connection.ConnectionString)));
        if (tags.Count == 0)
        {
            MessageBox.Show("No tags found.");
            return;
        }

        var tagFilterWindow = new TagFilterWindow(tags);
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
    ///     Event handler for the selection change in the list of notes.
    ///     Updates the current note based on the selection.
    /// </summary>
    private void lstNotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var lstNotesBox = (ListBox)sender;

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
        var lstTagsBox = (ListBox)sender;

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
        var button = (Button)sender;

        var stackPanel = (StackPanel)button.Parent;

        var note = (Note)stackPanel.DataContext;

        var tagExpand = new ExpandedNote(note);

        tagExpand.ShowDialog();

        this.viewModel.RefreshNotes();
    }

    #endregion
}