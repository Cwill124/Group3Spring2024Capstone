using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for EditNoteContent.xaml
    /// </summary>
    public partial class EditNoteContent : Window
    {
        public Note CurrentNote { get; set; }
        public EditNoteContent(Note note)
        {
            InitializeComponent();
            this.CurrentNote = note;
            this.titleTextBox.Text = this.CurrentNote.GetTitle();
            this.contentTextBox.Text = this.CurrentNote.GetContent();
        }

        private void UpdateNoteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var noteContentJson = new
            {
                note_Title = this.titleTextBox.Text,
                note_Content = this.contentTextBox.Text,
            };

            var jsonString = JsonConvert.SerializeObject(noteContentJson, Formatting.Indented);

            this.CurrentNote.Content = jsonString;
            
            DALConnection.NoteDAL.UpdateNoteContent(this.CurrentNote);

            this.Close();

        }
    }
}
