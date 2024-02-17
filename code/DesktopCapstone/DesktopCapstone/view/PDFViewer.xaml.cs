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
using DesktopCapstone.model;

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

            if ((int)source.SourceType == 2)
            {
                this.viewModel.CurrentSourceId = (int)source.SourceId;
                this.switchToVideoViewer();
            }
            else
            {
                this.viewModel.CurrentSourceId = (int)source.SourceId;
                this.webPDF.Source = this.viewModel.CurrentSourceLink;
            }

            this.viewModel.CurrentSourceId = (int)source.SourceId;
            this.webPDF.Source = this.viewModel.CurrentSourceLink;
        }

        private void switchToVideoViewer()
        {
            VideoViewer viewer = new VideoViewer(this.viewModel.CurrentSourceId, this.username);
            viewer.Show();
            this.Close();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            SourcesViewer viewer = new SourcesViewer(this.username);
            viewer.Show();
            this.Close();
        }
    }
}
