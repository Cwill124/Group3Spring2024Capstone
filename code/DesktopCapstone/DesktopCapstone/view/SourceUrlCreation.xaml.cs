using System.Diagnostics.CodeAnalysis;
using System.Windows;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
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

    private readonly int sourceType;
    private readonly string username;

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
    public SourceUrlCreation(int sourceType, string username)
    {
        //this.sourceType = sourceType;
        this.InitializeComponent();
        this.sourceType = sourceType;
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

        var content = JsonConvert.SerializeObject(new { url = this.txtUrl.Text, file = " " });
        var metaData = JsonConvert.SerializeObject(new
        {
            author = this.txtAuthor.Text, publisher = this.txtPublisher.Text, publisherYear = this.txtPublisherYear.Text
        });

        var sourceToAdd = new Source
        {
            SourceId = null,
            Name = this.txtName.Text,
            Content = content,
            MetaData = metaData,
            CreatedBy = this.username,
            Description = string.Empty,
            SourceTypeId = this.sourceType
        };
        //SourceDAL dal = new SourceDAL();
        DALConnection.SourceDAL.CreateSource(sourceToAdd);
        Close();
    }

    #endregion
}