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
        private Project selectedProject;

        public ProjectsViewer(string username)
        {
            InitializeComponent();
            this.selectedProject = null;
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

        private void ListBox_Click(object sender, SelectionChangedEventArgs e)
        {
            if (this.selectedProject == (Project)lstProjects.SelectedItem)
            {
                var singleProjectViewer = new SingleProjectViewer(this.selectedProject, username);
                singleProjectViewer.Show();
                this.Close();
            }
            else
            {
                this.selectedProject = (Project)lstProjects.SelectedItem;
            }

        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            var newPage = new Main(this.username);
            newPage.Show();
            Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.DeleteProject(this.selectedProject);
            this.viewModel.RefreshProjects();

        }

        private void lstProjects_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listBoxItem = GetVisualParent<ListBoxItem>(e.OriginalSource as DependencyObject);

            var project = listBoxItem?.DataContext as Project;

            if (this.selectedProject == project && project != null)
            {
                var singleProjectViewer = new SingleProjectViewer(this.selectedProject, username);
                singleProjectViewer.Show();
                this.Close();
            }
            else
            {
                this.selectedProject = project;
            }
        }

        private static T GetVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            while ((child != null) && !(child is T))
            {
                child = VisualTreeHelper.GetParent(child);
            }
            return child as T;
        }
    }
}
