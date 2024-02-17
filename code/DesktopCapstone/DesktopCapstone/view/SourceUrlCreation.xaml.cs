using desktop_capstone.DAL;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Newtonsoft.Json;
using System.Windows;


namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for SourceUrlCreation.xaml
    /// </summary>
    public partial class SourceUrlCreation : Window
    {
        private int sourceType;
        private string username;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceUrlCreation"/> class with default values.
        /// </summary>
        public SourceUrlCreation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceUrlCreation"/> class with specified parameters.
        /// </summary>
        /// <param name="sourceType">The type of the source.</param>
        /// <param name="username">The username associated with the source creation.</param>
        public SourceUrlCreation(int sourceType, string username)
        {
            //this.sourceType = sourceType;
            InitializeComponent();
            this.sourceType = sourceType;
            this.username = username;
        }

        /// <summary>
        /// Event handler for the "Create" button click.
        /// Serializes input data and creates a new source using the SourceDAL.
        /// Closes the current window after source creation.
        /// </summary>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var content = JsonConvert.SerializeObject(new { url = this.txtUrl.Text, file = " " });
            var metaData = JsonConvert.SerializeObject(new { author = txtAuthor.Text, publisher = txtPublisher.Text, publisherYear = txtPublisherYear.Text });

            var sourceToAdd = new Source
            {
                SourceId = null,
                Name = this.txtName.Text,
                Content = content,
                MetaData = metaData,
                CreatedBy = this.username,
                Description = String.Empty,
                SourceType = this.sourceType,
            };

            //SourceDAL dal = new SourceDAL();
            DALConnection.SourceDAL.CreateSource(sourceToAdd);
            this.Close();
        }
    }
}
