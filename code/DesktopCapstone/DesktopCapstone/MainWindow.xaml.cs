using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Dapper;

namespace DesktopCapstone;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class MainWindow : Window
{
    #region Constructors

    public MainWindow()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        this.InitializeComponent();
    }

    #endregion
}