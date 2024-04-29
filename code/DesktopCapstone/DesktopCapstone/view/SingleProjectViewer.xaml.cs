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
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using DesktopCapstone.viewmodel;
using MessageBox = System.Windows.MessageBox;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for SingleProjectViewerModel.xaml
    /// </summary>
    public partial class SingleProjectViewer : Window
    {
        private string username;
        private Project project;
        private SingleProjectViewerModel viewModel;
        private Source selectedSource;
        public SingleProjectViewer(Project project, string username)
        {
            this.viewModel = new SingleProjectViewerModel(project, DALConnection.ProjectDAL, username);
            this.DataContext = this.viewModel;
            this.selectedSource = null;
            InitializeComponent();
            this.username = username;
            this.project = project;
            
        }

        private void btnAddToProject_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.LoadUsersSources();
            var addSourceWindow = new AddProjectSource(this.viewModel.UsersSources, this.project.ProjectId.Value);
            addSourceWindow.ShowDialog();
            this.viewModel.LoadProjectSources();
        }

        private void btnRemoveFromProject_Click(object sender, RoutedEventArgs e)
        {
            if (this.lstProjectSources.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("Please select a source to remove.");
                return;
            }
            var selectedSources = this.lstProjectSources.SelectedItems.Cast<Source>().ToList();
            this.viewModel.RemoveSourcesFromProject(selectedSources);
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            var newPage = new ProjectsViewer(this.username);
            newPage.Show();
            Close();
        }

        private void btnExportProject_Click(object sender, RoutedEventArgs e)
        {
            //var exportText = this.viewModel.CreateProjectSourcesExport();
            //System.Windows.MessageBox.Show(exportText);
            var exportWindow = new ExportProjectWindow(this.viewModel.CreateProjectSourcesExport());
            exportWindow.ShowDialog();
        }

        private void lstProjectSources_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listBoxItem = GetVisualParent<ListBoxItem>(e.OriginalSource as DependencyObject);

            var source = listBoxItem?.DataContext as Source;

            if (this.selectedSource == source && source != null)
            {
                var sourceId = (int)source.SourceId;
                var singleProjectViewer = new Viewer(sourceId, username, this.selectedSource.SourceTypeId);
                singleProjectViewer.Show();
                this.Close();
            }
            else
            {
                this.selectedSource = source;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.DeleteProject();
            var newPage = new ProjectsViewer(this.username);
            newPage.Show();
            Close();
        }
    }
}
