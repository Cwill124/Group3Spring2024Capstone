using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Newtonsoft.Json;
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

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for NoteCreation.xaml
    /// </summary>
    public partial class NoteCreation : Window
    {

        private int currentSourceId;
        private string username;
        public NoteCreation()
        {
            InitializeComponent();
            this.currentSourceId = 0;
            this.username = string.Empty;
        }

        public NoteCreation(int currentSourceId, string username)
        {
            InitializeComponent();
            this.currentSourceId = currentSourceId;
            this.username = username;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var title = this.txtTitle.Text;
            var textContent = this.txtContent.Text;         
            var content = JsonConvert.SerializeObject(new { noteTitle = title, noteContent = textContent });

            var noteToAdd = new Note
            {
                Content = content,
                SourceId = this.currentSourceId,
                Username = this.username
            };
            NoteDAL dal = new NoteDAL();
            dal.CreateNote(noteToAdd);
            this.Close();
        }
    }
}
