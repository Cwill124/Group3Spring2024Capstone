using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Newtonsoft.Json;
using Button = System.Windows.Controls.Button;

namespace DesktopCapstone.view;

/// <summary>
///     Interaction logic for NoteCreation.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class NoteCreation : Window
{
    #region Data members

    private readonly int currentSourceId;
    private readonly string username;
    private readonly ObservableCollection<string> tagsToCreateCollection = new();

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="NoteCreation" /> class with default values.
    /// </summary>
    public NoteCreation()
    {
        this.InitializeComponent();
        this.currentSourceId = 0;
        this.username = string.Empty;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="NoteCreation" /> class with specified parameters.
    /// </summary>
    /// <param name="currentSourceId">The ID of the current source.</param>
    /// <param name="username">The username associated with the note.</param>
    public NoteCreation(int currentSourceId, string username)
    {
        this.InitializeComponent();
        this.currentSourceId = currentSourceId;
        this.username = username;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Event handler for the "Create" button click.
    ///     Creates a new note based on user input and adds it to the database.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">The event data.</param>
    private void btnCreate_Click(object sender, RoutedEventArgs e)
    {
        var title = this.txtTitle.Text;
        var textContent = this.txtContent.Text;
        var content = JsonConvert.SerializeObject(new { note_Title = title, note_Content = textContent });

        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(textContent))
        {
            System.Windows.MessageBox.Show("Please enter a title and content for the note.");
            return;
        }
        var noteToAdd = new Note
        {
            Content = content,
            SourceId = this.currentSourceId,
            Username = this.username
        };
        var tags = this.tagsToCreateCollection;

        var newNote = DALConnection.NoteDAL.CreateNote(noteToAdd);

        foreach (var currentTag in tags)
        {
            var newTag = new Tags
            {
                Note = newNote.NoteId,
                Tag = currentTag
            };
            DALConnection.TagDal.CreateTag(newTag);
        }

        Close();
    }

    private void AddTag_OnClick(object sender, RoutedEventArgs e)
    {
        var createTagWindow = new TagCreation();
        createTagWindow.ShowDialog();
        var tag = createTagWindow.TagText;
        this.tagsToCreateCollection.Add(tag);
        this.createNoteTagListBox.ItemsSource = this.tagsToCreateCollection;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var stackPanel = (StackPanel)button.Parent;
        var textBlock = (TextBlock)stackPanel.Children[0];

        var text = textBlock.Text;

        this.tagsToCreateCollection.Remove(text);
    }

    #endregion
}