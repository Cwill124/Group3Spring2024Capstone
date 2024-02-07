using desktop_capstone.view;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private string username;
        public Main()
        {
            InitializeComponent();
        }

        public Main(string username)
        {
            InitializeComponent();
            this.username = username;
            this.lblUsername.Content = username;
        }

        private void btnSources_Click(object sender, RoutedEventArgs e)
        {
            SourcesViewer newPage = new SourcesViewer(this.username);
            newPage.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login newPage = new Login();
            newPage.Show();
            this.Hide();
        }

    }
}
