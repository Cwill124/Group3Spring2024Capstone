using System.Diagnostics.CodeAnalysis;
using System.Windows;
using DesktopCapstone.model;
using DesktopCapstone.viewmodel;

namespace DesktopCapstone.view;

/// <summary>
///     Interaction logic for SourceCreation.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class SourceCreation : Window
{
    #region Data members

    private readonly SourceCreationViewModel viewModel;
    private readonly string username;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="SourceCreation" /> class with default values.
    /// </summary>
    public SourceCreation()
    {
        this.InitializeComponent();
        this.viewModel = new SourceCreationViewModel();
        DataContext = this.viewModel;
        this.cmbSourceType.SelectedIndex = 0;
        this.cmbSourceFormat.SelectedIndex = 0;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="SourceCreation" /> class with the specified username.
    /// </summary>
    /// <param name="username">The username associated with the source creation.</param>
    public SourceCreation(string username)
    {
        this.InitializeComponent();
        this.viewModel = new SourceCreationViewModel();
        DataContext = this.viewModel;
        this.cmbSourceFormat.SelectedIndex = 0;
        this.cmbSourceType.SelectedIndex = 0;
        this.username = username;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Event handler for the "Create" button click.
    ///     Opens a URL or file creation dialog based on the selected source format.
    ///     Closes the current window after dialog completion.
    /// </summary>
    private void btnCreate_Click(object sender, RoutedEventArgs e)
    {
        var format = this.cmbSourceFormat.SelectedItem as string;
        var type = this.cmbSourceType.SelectedItem as SourceType;

        if (format.Equals("URL"))
        {
            var newDialog = new SourceUrlCreation(type!.SourceTypeId, this.username);
            newDialog.ShowDialog();

            Close();
        }
        else
        {
            var newDialog = new SourceFileCreation();
            newDialog.ShowDialog();

            Close();
        }
    }

    #endregion
}