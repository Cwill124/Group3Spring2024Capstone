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
        public SingleProjectViewer(Project project, string username)
        {
            this.viewModel = new SingleProjectViewerModel(project, DALConnection.ProjectDAL, username);
            this.DataContext = this.viewModel;
            InitializeComponent();
            this.username = username;
            this.project = project;
            //this.viewModel.LoadProjectSources();
            //this.viewModel.LoadUsersSources();
            //this.viewModel = new SingleProjectViewerModel(project, DALConnection.ProjectDAL, username);
            //this.DataContext = this.viewModel;
        }

        private void btnAddToProject_Click(object sender, RoutedEventArgs e)
        {
            if (this.lstUserSources.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("Please select a source to add.");
                return;
            }
            var selectedSources = (List<Source>)this.lstUserSources.SelectedItems.Cast<Source>().ToList();
            this.viewModel.AddSourcesToProject(selectedSources);
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
    }
}
