using DesktopCapstone.viewmodel;
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
    /// Interaction logic for SourceCreation.xaml
    /// </summary>
    public partial class SourceCreation : Window
    {
        private SourceCreationViewModel viewModel;
        private string username;
        public SourceCreation()
        {
            InitializeComponent();
            this.viewModel = new SourceCreationViewModel();
            this.DataContext = viewModel;
            this.cmbSourceFormat.SelectedIndex = 0;
        }

        public SourceCreation(string username)
        {
            InitializeComponent();
            this.viewModel = new SourceCreationViewModel();
            this.DataContext = viewModel;
            this.cmbSourceFormat.SelectedIndex = 0;
            this.username = username;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var format = this.cmbSourceFormat.SelectedItem as string;
            if (format.Equals("URL"))
            {
                SourceUrlCreation newDialog = new SourceUrlCreation();
                newDialog.ShowDialog();

                this.Close();
            } else
            {
                SourceFileCreation newDialog = new SourceFileCreation();
                newDialog.ShowDialog();

                this.Close();
            }
        }
    }
}
