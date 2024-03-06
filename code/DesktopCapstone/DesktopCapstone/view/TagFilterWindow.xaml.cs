using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using DesktopCapstone.model;

namespace DesktopCapstone.view;

/// <summary>
///     Interaction logic for TagFilterWindow.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class TagFilterWindow : Window
{
    #region Data members

    private readonly ObservableCollection<Tags> tags;

    #endregion

    #region Properties

    public Tags SelectedTag { get; private set; }

    #endregion

    #region Constructors

    public TagFilterWindow(ObservableCollection<Tags> tags)
    {
        this.InitializeComponent();

        this.tags = tags;
        this.cmbTag.ItemsSource = this.tags;
        this.cmbTag.SelectedIndex = 0;
    }

    #endregion

    #region Methods

    private void btnFilter_Click(object sender, RoutedEventArgs e)
    {
        this.SelectedTag = (Tags)this.cmbTag.SelectedItem;
        Close();
    }

    #endregion
}