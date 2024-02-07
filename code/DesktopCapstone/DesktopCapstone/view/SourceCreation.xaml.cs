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

        public SourceCreation()
        {
            this.viewModel = new SourceCreationViewModel();
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
