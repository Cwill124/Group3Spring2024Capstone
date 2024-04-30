using System.Diagnostics.CodeAnalysis;
using System.Windows;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using DesktopCapstone.viewmodel;
using Newtonsoft.Json;
using MessageBox = System.Windows.MessageBox;

namespace DesktopCapstone.view;

/// <summary>
///     Interaction logic for SourceUrlCreation.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class SourceUrlCreation : Window
{
    #region Data members

    private readonly string username;
    private SourceCreationViewModel viewModel;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="SourceUrlCreation" /> class with default values.
    /// </summary>
    public SourceUrlCreation()
    {
        this.InitializeComponent();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="SourceUrlCreation" /> class with specified parameters.
    /// </summary>
    /// <param name="sourceType">The type of the source.</param>
    /// <param name="username">The username associated with the source creation.</param>
    public SourceUrlCreation(string username)
    {
        //this.sourceType = sourceType;
        this.InitializeComponent();
        this.viewModel = new SourceCreationViewModel(DALConnection.SourceDAL);
        DataContext = this.viewModel;
        this.cmbSourceType.SelectedIndex = 0;
        this.username = username;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Event handler for the "Create" button click.
    ///     Serializes input data and creates a new source using the SourceDAL.
    ///     Closes the current window after source creation.
    /// </summary>
    private void btnCreate_Click(object sender, RoutedEventArgs e)
    {
        if (this.txtUrl.Text == string.Empty || this.txtName.Text == string.Empty)
        {
            MessageBox.Show("Url and Name cannot be empty.");
            return;
        }

        if (!Uri.IsWellFormedUriString(this.txtUrl.Text, UriKind.Absolute))
        {
            MessageBox.Show("Invalid URL.");
            return;
        }
        var type = this.cmbSourceType.SelectedItem as SourceType;

        var content = JsonConvert.SerializeObject(new { url = this.txtUrl.Text, file = " " });
        var metaData = "{}";
        if (this.txtAuthor.Text != string.Empty && this.txtPublisher.Text != string.Empty &&
                       this.txtPublisherYear.Text != string.Empty)
        {
            metaData = JsonConvert.SerializeObject(new
            {
                author = this.txtAuthor.Text, publisher = this.txtPublisher.Text, publisherYear = this.txtPublisherYear.Text
            });
        }
        //metaData = JsonConvert.SerializeObject(new
        //{
        //    author = this.txtAuthor.Text, publisher = this.txtPublisher.Text, publisherYear = this.txtPublisherYear.Text
        //});

        var sourceToAdd = new Source
        {
            SourceId = null,
            Name = this.txtName.Text,
            Content = content,
            MetaData = metaData,
            CreatedBy = this.username,
            Description = string.Empty,
            SourceTypeId = type!.SourceTypeId
        };
        //SourceDAL dal = new SourceDAL();
        DALConnection.SourceDAL.CreateSource(sourceToAdd);
        Close();
    }

    #endregion
}