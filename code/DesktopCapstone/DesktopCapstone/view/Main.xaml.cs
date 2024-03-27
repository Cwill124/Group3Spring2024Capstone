using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace DesktopCapstone.view;

/// <summary>
///     Interaction logic for the main window.
/// </summary>
[ExcludeFromCodeCoverage]
public partial class Main : Window
{
    #region Data members

    private readonly string username;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="Main" /> class without a specified username.
    /// </summary>
    public Main()
    {
        this.InitializeComponent();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Main" /> class with a specified username.
    ///     Sets up the username and updates the corresponding label in the UI.
    /// </summary>
    /// <param name="username">The username of the logged-in user.</param>
    public Main(string username)
    {
        this.InitializeComponent();
        this.username = username;
        this.lblUsername.Content = username;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Handles the click event for the "Sources" button.
    ///     Opens the SourcesViewer page, passing the current username.
    ///     Hides the current main window.
    /// </summary>
    private void btnSources_Click(object sender, RoutedEventArgs e)
    {
        var newPage = new SourcesViewer(this.username);
        newPage.Show();
        Hide();
    }

    /// <summary>
    ///     Handles the click event for the "Logout" button.
    ///     Opens the login page and hides the current main window.
    /// </summary>
    private void btnLogout_Click(object sender, RoutedEventArgs e)
    {
        var newPage = new Login();
        newPage.Show();
        Hide();
    }

    /// <summary>
    ///     Handles the click event for the "Logout" button in the menu.
    ///     Opens the login page and closes the current main window.
    /// </summary>
    private void btnLogout_Click_1(object sender, RoutedEventArgs e)
    {
        var newPage = new Login();
        newPage.Show();
        Close();
    }

    private void btnNotes_Click(object sender, RoutedEventArgs e)
    {
        var newPage = new NotesViewer(this.username);
        newPage.Show();
        Hide();
    }

    #endregion

    private void btnProjects_Click(object sender, RoutedEventArgs e)
    {
        var newPage = new ProjectsViewer(this.username);
        newPage.Show();
        Hide();
    }
}