using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using DesktopCapstone.model;
using DesktopCapstone.viewmodel;

namespace DesktopCapstone.view;

/// <summary>
///     Interaction logic for SourcesViewer.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class SourcesViewer : Window
{
    #region Data members

    private readonly SourcesViewerViewModel viewModel;
    private readonly string username;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="SourcesViewer" /> class with default values.
    /// </summary>
    public SourcesViewer()
    {
        this.InitializeComponent();
        //this.viewModel = new SourcesViewerViewModel();
        DataContext = this.viewModel;
        this.username = string.Empty;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="SourcesViewer" /> class with the specified username.
    /// </summary>
    /// <param name="username">The username associated with the sources viewer.</param>
    public SourcesViewer(string username)
    {
        this.InitializeComponent();
        this.viewModel = new SourcesViewerViewModel(username);
        DataContext = this.viewModel;
        this.username = username;
        this.lstSources.ItemsSource = this.viewModel.Sources;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Event handler for the "Add Source" button click.
    ///     Opens a dialog for creating a new source and refreshes the list of sources.
    /// </summary>
    private void btnAddSource_Click(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine("clicked");
        var sourceCreationDialog = new SourceCreation(this.username);
        sourceCreationDialog.ShowDialog();
        this.viewModel.RefreshSources();
    }

    /// <summary>
    ///     Event handler for the selection change in the list of sources.
    ///     Opens a PDF viewer window for the selected source and hides the current window.
    /// </summary>
    private void lstSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var source = (Source)this.lstSources.SelectedItem;
        var sourceId = (int)source.SourceId;
        var sourceType = source.SourceTypeId;
        var viewer = new Viewer(sourceId, this.username, sourceType);
        viewer.Show();
        Hide();
    }

    /// <summary>
    ///     Event handler for the "Return" button click.
    ///     Navigates to the main page and closes the current window.
    /// </summary>
    private void btnReturn_Click(object sender, RoutedEventArgs e)
    {
        var newPage = new Main(this.username);
        newPage.Show();
        Close();
    }

    #endregion
}