using DesktopCapstone.viewmodel;
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
using desktop_capstone.DAL;
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

        public Note? CurrentNote { get; private set; }


        public PDFViewer()
        {
            InitializeComponent();
            this.viewModel = new PDFViewerViewModel(this.currentSourceId);
            this.DataContext = viewModel;
        }

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

        private void btnAddSource_Click(object sender, RoutedEventArgs e)
        {
            SourceCreation sourceCreationDialog = new SourceCreation();
            sourceCreationDialog.ShowDialog();
            this.viewModel.refreshSources();
        }

        private void btnAddNotes_Click(object sender, RoutedEventArgs e)
        {
            NoteCreation noteCreationDialog = new NoteCreation(this.viewModel.CurrentSourceId, this.username);
            noteCreationDialog.ShowDialog();
            this.viewModel.refreshNotes();
        }

        private void lstSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var source = (Source)this.lstSources.SelectedItem;
            
            this.viewModel.CurrentSourceId = (int)source.SourceId;
            this.webPDF.Source = this.viewModel.CurrentSourceLink;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            SourcesViewer viewer = new SourcesViewer(this.username);
            viewer.Show();
            this.Close();
        }

        private void btnDelete_Source(object sender, RoutedEventArgs e)
        {
            DALConnection.SourceDAL.DeleteById(this.currentSourceId);   
            var viewer = new SourcesViewer(this.username);
            viewer.Show();
            this.Close();

        }

        private void btnDelete_Note(object sender, RoutedEventArgs e)
        {
            if (this.CurrentNote is not null)
            {
                DALConnection.NoteDAL.DeleteNoteById(this.CurrentNote.NoteId);
                this.viewModel.refreshNotes(); 
            }
        }

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

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button)
            {
                button.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 52, 152, 219));
                button.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255,255,255,255));
                button.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 255, 255, 255));
            }
        }

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
