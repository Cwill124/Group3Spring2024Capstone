using desktop_capstone.DAL;
using DesktopCapstone.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
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
            this.sourceType = sourceType;
            InitializeComponent();
            this.username = username;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            //var content = "{\"url\": \"John\", \"file\": 30}";
            //var metaData = "Author: " + this.txtAuthor.Text
                //+ " Publisher: " + this.txtPublisher.Text
                //+ " Publisher Year: " + this.txtPublisherYear.Text;
            //var metaData = "{\"author\": \"John\", \"publisher\": 30, \"publisher year\": \"New York\"}";
            //var jsonContent = JObject.Parse(content);
            //var jsonMetaData = JObject.Parse(metaData);
            var content = JsonConvert.SerializeObject( new {url = this.txtUrl.Text, file = " " });
            var metaData = JsonConvert.SerializeObject(new {author = txtAuthor.Text, publisher = txtPublisher.Text, publisherYear = txtPublisherYear.Text });
            

            //var url = new Uri(metaData);
            var sourceToAdd = new Source(" ",this.txtName.Text, content, metaData, 1, "{\"tags\": \"empty\"}", this.username);
            SourceDAL dal = new SourceDAL();
            dal.addNewSource(sourceToAdd);
            this.Close();
        }
    }
}
