using System.Diagnostics.CodeAnalysis;
using System.Windows;
namespace DesktopCapstone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            this.InitializeComponent();
        }
    }
}