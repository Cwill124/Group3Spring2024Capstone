using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.MessageBox;

namespace DesktopCapstone.view;

/// <summary>
///     Interaction logic for ExpandedNote.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class ExpandedNote : Window
{
    #region Data members

    private readonly Note currentNote;

    #endregion

    #region Properties

    public ObservableCollection<Tags> currentNoteTags { get; }

    #endregion

    #region Constructors

    public ExpandedNote(Note currentNote)
    {
        this.InitializeComponent();

        this.currentNote = currentNote;
        this.currentNoteTags = new ObservableCollection<Tags>();
        this.refreshTags();
        this.noteTitleLabel.Content = this.currentNote.GetTitle();
        this.noteContentTextBlock.Text = this.currentNote.GetContent();
    }

    #endregion

    #region Methods

    private void refreshTags()
    {
        this.taglst.Items.Clear();
        this.taglst.ItemsSource = this.currentNoteTags;
        foreach (var VARIABLE in this.currentNote.TagList)
        {
            this.currentNoteTags.Add(VARIABLE);
        }
    }

    private void DeleteTagButton_OnClick(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;

        var stackPanel = (StackPanel)button.Parent;
        var textBlock = (TextBlock)stackPanel.Children[0];

        var tag = (Tags)textBlock.DataContext;

        DALConnection.TagDal.DeleteTag(tag);
        this.currentNoteTags.Remove(tag);
    }

    private void btn_AddTag(object sender, RoutedEventArgs e)
    {
        var addDialog = new TagCreation();

        addDialog.ShowDialog();

        var tag = addDialog.TagText;

        var newTag = new Tags
        {
            Note = this.currentNote.NoteId,
            Tag = tag
        };
        if (newTag.Tag == String.Empty)
        {
            MessageBox.Show("Tag name cannot be empty");
            return;
        }
        DALConnection.TagDal.CreateTag(newTag);
        this.currentNoteTags.Add(newTag);
    }

    private void EditNoteButton_OnClick(object sender, RoutedEventArgs e)
    {
        var editDialog = new EditNoteContent(this.currentNote);

        editDialog.ShowDialog();

        var newContent = editDialog.CurrentNote.Content;

        this.currentNote.Content = newContent;

        this.noteContentTextBlock.Text = this.currentNote.GetContent();
        this.noteTitleLabel.Content = this.currentNote.GetTitle();
    }

    #endregion
}