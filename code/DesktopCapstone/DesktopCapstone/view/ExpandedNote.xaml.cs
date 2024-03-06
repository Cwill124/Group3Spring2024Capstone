using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Button = System.Windows.Controls.Button;
using TextBox = System.Windows.Controls.TextBox;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for ExpandedNote.xaml
    /// </summary>
    public partial class ExpandedNote : Window
    {
        private Note currentNote;
        public ObservableCollection<Tags> currentNoteTags { get; private set; }

        public ExpandedNote(Note currentNote)
        {
            InitializeComponent();

            this.currentNote = currentNote;
            this.currentNoteTags = new ObservableCollection<Tags>();
            this.refreshTags();
            this.noteTitleLabel.Content = this.currentNote.GetTitle();
            this.noteContentTextBlock.Text = this.currentNote.GetContent();
        }

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
            Button button = (Button)sender;

            StackPanel stackPanel = (StackPanel)button.Parent;
            TextBlock textBlock = (TextBlock)stackPanel.Children[0];

            var tag = (Tags)textBlock.DataContext;

            DALConnection.TagDal.DeleteTag(tag);
            this.currentNoteTags.Remove(tag);

        }

        private void btn_AddTag(object sender, RoutedEventArgs e)
        {
            var addDialog = new TagCreation();

            addDialog.ShowDialog();

            var tag = addDialog.TagText;

            var newTag = new Tags()
            {
                Note = this.currentNote.NoteId,
                Tag = tag
            };
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
    }
}
