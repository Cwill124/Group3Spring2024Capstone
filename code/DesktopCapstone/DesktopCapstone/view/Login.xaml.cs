using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using DesktopCapstone.DAL;
using DesktopCapstone.model;
using Npgsql;
using MessageBox = System.Windows.MessageBox;

namespace DesktopCapstone.view;

/// <summary>
///     Interaction logic for the login window.
/// </summary>
[ExcludeFromCodeCoverage]
public partial class Login : Window
{
    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="Login" /> class.
    /// </summary>
    public Login()
    {
        this.InitializeComponent();
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Handles the click event for the login button.
    ///     Attempts to log in using the provided username and password.
    ///     If successful, opens the main page; otherwise, displays an error message.
    /// </summary>
    private void btnLogin_Click(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine("Login pressed");

        var validLogin = this.handleLogin();

        if (validLogin != null)
        {
            var newPage = new Main(this.txtUsername.Text);
            newPage.Show();
            Close();
        }
        else
        {
            MessageBox.Show("Invalid username or password");
        }
    }

    /// <summary>
    ///     Handles the login process by retrieving the username and password from the input fields.
    ///     Calls the LoginDAL to check the login credentials.
    /// </summary>
    /// <returns>The logged-in user if successful; otherwise, returns null.</returns>
    private AppUser handleLogin()
    {
        var username = this.txtUsername.Text;
        var password = this.txtPassword.Password;

        AppUser loginResult = null;

        try
        {
            loginResult =
                new LoginDAL(new NpgsqlConnection(Connection.ConnectionString)).checkLogin(username, password);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Connection failed");
        }

        return loginResult;
    }

    /// <summary>
    ///     Handles the click event for the sign-up button.
    ///     Opens the sign-up page.
    /// </summary>
    private void btnSignUp_Click(object sender, RoutedEventArgs e)
    {
        var signUpPage = new SignUp();
        signUpPage.Show();

        Close();
    }

    #endregion
}