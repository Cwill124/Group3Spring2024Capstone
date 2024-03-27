using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using DesktopCapstone.viewmodel;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for ProjectsViewer.xaml
    /// </summary>
    public partial class ProjectsViewer : Window
    {
        private string username;
        private ProjectsViewerViewModel viewModel;
        public ProjectsViewer(string username)
        {
            InitializeComponent();
            this.username = username;
            this.viewModel = new ProjectsViewerViewModel(username, DALConnection.ProjectDAL);
            this.DataContext = this.viewModel;
            this.viewModel.RefreshProjects();
            this.lstProjects.ItemsSource = this.viewModel.Projects;
           
        }

        private void btnAddProject_Click(object sender, RoutedEventArgs e)
        {
            var projectCreationWindow = new ProjectCreationWindow(username);
            projectCreationWindow.ShowDialog();
            this.viewModel.RefreshProjects();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var project = (Project)lstProjects.SelectedItem;
            var singleProjectViewer = new SingleProjectViewer(project, username);
            singleProjectViewer.Show();
            this.Close();

        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            var newPage = new Main(this.username);
            newPage.Show();
            Close();
        }
    }
}
