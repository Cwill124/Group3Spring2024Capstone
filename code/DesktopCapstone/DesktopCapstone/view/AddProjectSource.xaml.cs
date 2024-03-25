using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddProjectSource.xaml
    /// </summary>
    public partial class AddProjectSource : Window
    {

        private ObservableCollection<Source> sources;
        private int projectId;

        public AddProjectSource(ObservableCollection<Source> sources, int projectId)
        {
            InitializeComponent();
            this.sources = sources;
            this.lstUserSources.ItemsSource = sources;
            this.projectId = projectId;
        }

        private void btnAddProject_Click(object sender, RoutedEventArgs e)
        {
            var selected = this.lstUserSources.SelectedItems.Cast<Source>().ToList();
            foreach (var current in selected)
            {
                DALConnection.ProjectDAL.AddSourceToProject(this.projectId, current.SourceId.Value);
            }
            this.Close();
        }
    }
} 
