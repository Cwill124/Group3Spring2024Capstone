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

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for ProjectCreationWindow.xaml
    /// </summary>
    public partial class ProjectCreationWindow : Window
    {
        private string username;
        public ProjectCreationWindow(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void btnAddProject_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProjectName.Text))
            {
                System.Windows.MessageBox.Show("Please enter a project name.");
                return;
            }

            Project project = new Project
            {
                Title = txtProjectName.Text,
                Description = txtProjectDescription.Text,
                Owner = this.username
            };

            DALConnection.ProjectDAL.AddProject(project);
            this.Close();
        }
    }
}
