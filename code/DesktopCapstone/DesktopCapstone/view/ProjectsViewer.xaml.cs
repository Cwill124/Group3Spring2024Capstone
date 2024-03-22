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
    /// Interaction logic for ProjectsViewer.xaml
    /// </summary>
    public partial class ProjectsViewer : Window
    {
        private string username;
        private ProjectsViewer viewModel;
        public ProjectsViewer(string username)
        {
            InitializeComponent();
            this.username = username;
            this.viewModel = new ProjectsViewer(username);
            this.DataContext = this.viewModel;
        }

        private void btnAddProject_Click(object sender, RoutedEventArgs e)
        {
            var projectCreationWindow = new ProjectCreationWindow(username);
            projectCreationWindow.ShowDialog();
            this.viewModel.RefreshProjects();
        }
    }
}
