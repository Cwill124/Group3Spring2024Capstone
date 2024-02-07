using DesktopCapstone.viewmodel;
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

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for SourcesViewer.xaml
    /// </summary>
    public partial class SourcesViewer : Window
    {
        private SourcesViewerViewModel viewModel;
        private string username;
        public SourcesViewer()
        {
            InitializeComponent();
            this.viewModel = new SourcesViewerViewModel();
            this.DataContext = viewModel;
            this.username = string.Empty;
            //this.lstSources.ItemsSource = this.viewModel.Sources;
        }

        public SourcesViewer(string username)
        {
            InitializeComponent();
            this.viewModel = new SourcesViewerViewModel();
            this.DataContext = viewModel;
            this.username = username;
            Debug.WriteLine(this.lstSources.Items.Count);
            this.lstSources.ItemsSource = this.viewModel.Sources;
            //this.lstSources.ItemsSource = this.viewModel.Sources;
        }

        private void btnAddSource_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("clicked");
            SourceCreation sourceCreationDialog = new SourceCreation(this.username);
            sourceCreationDialog.ShowDialog();
            this.viewModel.refreshSources();

        }

        private void lstSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int sourceId = this.lstSources.SelectedIndex;
            PDFViewer viewer = new PDFViewer(sourceId + 1, this.username);
            viewer.Show();
            this.Hide();
        }
    }
}
