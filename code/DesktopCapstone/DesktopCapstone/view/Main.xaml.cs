using desktop_capstone.view;
using System.Windows;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for the main window.
    /// </summary>
    public partial class Main : Window
    {
        private string username;

        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class without a specified username.
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class with a specified username.
        /// Sets up the username and updates the corresponding label in the UI.
        /// </summary>
        /// <param name="username">The username of the logged-in user.</param>
        public Main(string username)
        {
            InitializeComponent();
            this.username = username;
            this.lblUsername.Content = username;
        }

        /// <summary>
        /// Handles the click event for the "Sources" button.
        /// Opens the SourcesViewer page, passing the current username.
        /// Hides the current main window.
        /// </summary>
        private void btnSources_Click(object sender, RoutedEventArgs e)
        {
            SourcesViewer newPage = new SourcesViewer(this.username);
            newPage.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the click event for the "Logout" button.
        /// Opens the login page and hides the current main window.
        /// </summary>
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login newPage = new Login();
            newPage.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the click event for the "Logout" button in the menu.
        /// Opens the login page and closes the current main window.
        /// </summary>
        private void btnLogout_Click_1(object sender, RoutedEventArgs e)
        {
            Login newPage = new Login();
            newPage.Show();
            this.Close();
        }
    }
}
