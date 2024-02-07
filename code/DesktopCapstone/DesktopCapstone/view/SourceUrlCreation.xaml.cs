using DesktopCapstone.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        public SourceUrlCreation()
        {
            InitializeComponent();
        }

        public SourceUrlCreation(int sourceType)
        {
            this.sourceType = sourceType;
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var metaData = "Author: " + this.txtAuthor.Text
                + " Publisher: " + this.txtPublisher.Text
                + " Publisher Year: " + this.txtPublisherYear.Text;
            //JsonSerializer serializer = ;
            var url = new Uri(metaData);
            var SourceToAdd = new Source(this.txtName.Text, );
        }
    }
}
