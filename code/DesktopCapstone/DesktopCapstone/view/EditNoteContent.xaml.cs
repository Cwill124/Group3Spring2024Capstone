using System.Diagnostics.CodeAnalysis;
using System.Windows;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Newtonsoft.Json;

namespace DesktopCapstone.view;

/// <summary>
///     Interaction logic for EditNoteContent.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class EditNoteContent : Window
{
    #region Properties

    public Note CurrentNote { get; set; }

    #endregion

    #region Constructors

    public EditNoteContent(Note note)
    {
        this.InitializeComponent();
        this.CurrentNote = note;
        this.titleTextBox.Text = this.CurrentNote.GetTitle();
        this.contentTextBox.Text = this.CurrentNote.GetContent();
    }

    #endregion

    #region Methods

    private void UpdateNoteButton_OnClick(object sender, RoutedEventArgs e)
    {
        var noteContentJson = new
        {
            note_Title = this.titleTextBox.Text,
            note_Content = this.contentTextBox.Text
        };

        var jsonString = JsonConvert.SerializeObject(noteContentJson, Formatting.Indented);

        this.CurrentNote.Content = jsonString;

        DALConnection.NoteDAL.UpdateNoteContent(this.CurrentNote);

        Close();
    }

    #endregion
}