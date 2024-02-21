using desktop_capstone.DAL;
using desktop_capstone.model;
using DesktopCapstone.view;
using System;
using System.Diagnostics;
using System.Windows;

namespace desktop_capstone.view
{
    /// <summary>
    /// Interaction logic for the login window.
    /// </summary>
    public partial class Login : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the click event for the login button.
        /// Attempts to log in using the provided username and password.
        /// If successful, opens the main page; otherwise, displays an error message.
        /// </summary>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Login pressed");

            var validLogin = this.handleLogin();

            if (validLogin != null)
            {
                Main newPage = new Main(this.txtUsername.Text);
                newPage.Show();
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Invalid username or password");
            }
        }

        /// <summary>
        /// Handles the login process by retrieving the username and password from the input fields.
        /// Calls the LoginDAL to check the login credentials.
        /// </summary>
        /// <returns>The logged-in user if successful; otherwise, returns null.</returns>
        private model.AppUser handleLogin()
        {
            var username = this.txtUsername.Text;
            var password = this.txtPassword.Password;

            AppUser loginResult = null;

            try
            {
                loginResult = new LoginDAL().checkLogin(username, password);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Connection failed");
            }
            return loginResult;
        }

        /// <summary>
        /// Handles the click event for the sign-up button.
        /// Opens the sign-up page.
        /// </summary>
        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUpPage = new SignUp();
            signUpPage.Show();

            this.Close();
        }
    }
}
