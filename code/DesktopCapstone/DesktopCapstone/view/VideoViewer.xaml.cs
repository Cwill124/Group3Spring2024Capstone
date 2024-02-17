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
using System.Windows.Shapes;
using DesktopCapstone.model;
using DesktopCapstone.viewmodel;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for VideoViewer.xaml
    /// </summary>
    public partial class VideoViewer : Window
    {
        private int currentSourceId;
        private string username;
        private PDFViewerViewModel viewModel;

        public VideoViewer(int currentSourceId, string username)
        {
            InitializeComponent();
            this.currentSourceId = currentSourceId;
            this.username = username;
            this.viewModel = new PDFViewerViewModel(this.currentSourceId);
            this.DataContext = viewModel;
            this.lstSources.ItemsSource = this.viewModel.Sources;
            this.lstNotes.ItemsSource = this.viewModel.Notes;
            this.loadVideo();
        }

        private void btnAddSource_Click(object sender, RoutedEventArgs e)
        {
            SourceCreation sourceCreationDialog = new SourceCreation();
            sourceCreationDialog.ShowDialog();
            this.viewModel.RefreshSources();
        }

        private void btnAddNotes_Click(object sender, RoutedEventArgs e)
        {
            NoteCreation noteCreationDialog = new NoteCreation(this.viewModel.CurrentSourceId, this.username);
            noteCreationDialog.ShowDialog();
            this.viewModel.RefreshNotes();
        }

        private void lstSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var source = (Source)this.lstSources.SelectedItem;

            if ((int)source.SourceType == 1)
            {
                this.viewModel.CurrentSourceId = (int)source.SourceId;
                this.switchToPDFViewer();
            }
            else
            {
                this.viewModel.CurrentSourceId = (int)source.SourceId;
                this.loadVideo();
            }
        }   

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            SourcesViewer viewer = new SourcesViewer(this.username);
            viewer.Show();
            this.Close();
        }

        private void switchToPDFViewer()
        {
            PDFViewer viewer = new PDFViewer(this.viewModel.CurrentSourceId, this.username);
            viewer.Show();
            this.Close();
        }
        

        private void loadVideo()
        {
            if (this.viewModel.CurrentSourceLink.ToString().Contains("youtube"))
            {
                var youtubeLink = this.viewModel.CurrentSourceLink.ToString();
                var youtubeId = this.convertYoutubeLinkToId(youtubeLink);
                this.loadEmbeddedYoutubeVideo(youtubeId);
            }
            else
            {
                this.webVideo.Source = this.viewModel.CurrentSourceLink;
            }
        }

        private void loadEmbeddedYoutubeVideo(string youtubeId)
        {
            var youtubeLink = "https://www.youtube.com/embed/" + youtubeId;
            this.viewModel.CurrentSourceLink = new Uri(youtubeLink);
            this.webVideo.Source = this.viewModel.CurrentSourceLink;
        }

        private string convertYoutubeLinkToId(string youtubeLink)
        {
            var id = youtubeLink.Substring(youtubeLink.IndexOf('=') + 1);
            return id;
        }
    }
}