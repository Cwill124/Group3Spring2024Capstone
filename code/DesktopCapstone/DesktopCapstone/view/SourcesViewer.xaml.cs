using DesktopCapstone.viewmodel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using DesktopCapstone.model;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for SourcesViewer.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class SourcesViewer : Window
    {
        private SourcesViewerViewModel viewModel;
        private string username;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesViewer"/> class with default values.
        /// </summary>
        public SourcesViewer()
        {
            InitializeComponent();
            this.viewModel = new SourcesViewerViewModel();
            this.DataContext = viewModel;
            this.username = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesViewer"/> class with the specified username.
        /// </summary>
        /// <param name="username">The username associated with the sources viewer.</param>
        public SourcesViewer(string username)
        {
            InitializeComponent();
            this.viewModel = new SourcesViewerViewModel();
            this.DataContext = viewModel;
            this.username = username;
            this.lstSources.ItemsSource = this.viewModel.Sources;
        }

        /// <summary>
        /// Event handler for the "Add Source" button click.
        /// Opens a dialog for creating a new source and refreshes the list of sources.
        /// </summary>
        private void btnAddSource_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("clicked");
            SourceCreation sourceCreationDialog = new SourceCreation(this.username);
            sourceCreationDialog.ShowDialog();
            this.viewModel.RefreshSources();
        }

        /// <summary>
        /// Event handler for the selection change in the list of sources.
        /// Opens a PDF viewer window for the selected source and hides the current window.
        /// </summary>
        private void lstSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var source = (Source)this.lstSources.SelectedItem;
            var sourceId = (int)source.SourceId;
            var sourceType = source.SourceType;
            Viewer viewer = new Viewer(sourceId, this.username, sourceType);
            viewer.Show();
            this.Hide();
        }

        /// <summary>
        /// Event handler for the "Return" button click.
        /// Navigates to the main page and closes the current window.
        /// </summary>
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            Main newPage = new Main(this.username);
            newPage.Show();
            this.Close();
        }
    }
}
