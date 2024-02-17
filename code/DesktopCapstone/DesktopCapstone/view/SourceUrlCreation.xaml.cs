using desktop_capstone.DAL;
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

        public SourceUrlCreation()
        {
            InitializeComponent();
        }

        public SourceUrlCreation(int sourceType, string username)
        {
            //this.sourceType = sourceType;
            InitializeComponent();
            this.sourceType = sourceType;
            this.username = username;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var content = JsonConvert.SerializeObject( new {url = this.txtUrl.Text, file = " " });
            var metaData = JsonConvert.SerializeObject(new {author = txtAuthor.Text, publisher = txtPublisher.Text, publisherYear = txtPublisherYear.Text });
            
            //var sourceToAdd = new Source(" ",this.txtName.Text, content, metaData, 1, "{\"tags\": \"empty\"}", this.username);
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
            
            SourceDAL dal = new SourceDAL();
            dal.addNewSource(sourceToAdd);
            this.Close();
        }
    }
}
