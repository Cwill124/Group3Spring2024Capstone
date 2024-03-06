using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DesktopCapstone.model;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for TagFilterWindow.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class TagFilterWindow : Window
    {
        private ObservableCollection<Tags> tags;
        public Tags SelectedTag { get; private set; }

        public TagFilterWindow(ObservableCollection<Tags> tags)
        {
            InitializeComponent();
            
            this.tags = tags;
            this.cmbTag.ItemsSource = this.tags;
            this.cmbTag.SelectedIndex = 0;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            SelectedTag = (Tags)this.cmbTag.SelectedItem;
            this.Close();
        }
    }
}
