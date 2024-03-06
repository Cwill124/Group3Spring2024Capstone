using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace DesktopCapstone.view;

/// <summary>
///     Interaction logic for TagCreation.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class TagCreation : Window
{
    #region Properties

    public string? TagText { get; set; }

    #endregion

    #region Constructors

    public TagCreation()
    {
        this.InitializeComponent();
    }

    #endregion

    #region Methods

    private void CreateButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.TagText = this.tagNameTextbox.Text;
        Close();
    }

    #endregion
}