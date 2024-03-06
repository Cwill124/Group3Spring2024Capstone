using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Windows;
using DesktopCapstone.DAL;
using Npgsql;

namespace DesktopCapstone.view;

/// <summary>
///     Interaction logic for Signup.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class SignUp : Window
{
    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="SignUp" /> class.
    /// </summary>
    public SignUp()
    {
        this.InitializeComponent();
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Event handler for the "Sign Up" button click.
    ///     Checks for input errors, creates a new user, and navigates to the login page upon success.
    /// </summary>
    private void btnSignUp_Click(object sender, RoutedEventArgs e)
    {
        if (this.CheckForErrors())
        {
            return;
        }

        var dal = new AppUserDAL(new NpgsqlConnection(Connection.ConnectionString));
        var creationSuccess = dal.CreateNewUser(this.txtUsername.Text, this.txtPassword.Password,
            this.txtFirstName.Text, this.txtLastName.Text, this.txtEmail.Text, this.txtPhoneNumber.Text);

        if (creationSuccess)
        {
            var newPage = new Login();
            newPage.Show();
            Hide();
        }
    }

    /// <summary>
    ///     Event handler for the "Return" button click.
    ///     Navigates to the login page.
    /// </summary>
    private void btnReturn_Click(object sender, RoutedEventArgs e)
    {
        var newPage = new Login();
        newPage.Show();
        Hide();
    }

    /// <summary>
    ///     Checks for input errors and displays corresponding error messages.
    /// </summary>
    /// <returns>True if there are errors, otherwise false.</returns>
    private bool CheckForErrors()
    {
        var hasErrors = false;
        var dal = new LoginDAL(new NpgsqlConnection(Connection.ConnectionString));

        this.lblErrorFirstName.Text = "";
        this.lblErrorLastName.Text = "";
        this.lblErrorUsername.Text = "";
        this.lblErrorPassword.Text = "";
        this.lblErrorPhone.Text = "";
        this.lblErrorEmail.Text = "";

        var usernameRegex = new Regex("^[a-zA-Z0-9]{5,}$");
        var passwordRegex = new Regex("^(?=.*[A-Z])(?=.*\\d).{8,}$");
        var emailRegex = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
        var phoneRegex = new Regex("^\\d{10}$");

        if (!usernameRegex.IsMatch(this.txtUsername.Text))
        {
            this.lblErrorUsername.Text = "At least 5 characters long and only letters, numbers, and underscores";
            hasErrors = true;
        }

        if (dal.checkIfUsernameIsInUse(this.txtUsername.Text))
        {
            this.lblErrorUsername.Text = "Username is already in use";
            hasErrors = true;
        }

        if (!passwordRegex.IsMatch(this.txtPassword.Password))
        {
            this.lblErrorPassword.Text = "At least 8 characters long and at least one uppercase letter and one number";
            hasErrors = true;
        }

        if (this.txtPassword.Password != this.txtRePassword.Password)
        {
            this.lblErrorPassword.Text = "Passwords do not match";
            hasErrors = true;
        }

        if (this.txtFirstName.Text == "")
        {
            this.lblErrorFirstName.Text = "First Name is required";
            hasErrors = true;
        }

        if (this.txtLastName.Text == "")
        {
            this.lblErrorLastName.Text = "Last Name is required";
            hasErrors = true;
        }

        if (!emailRegex.IsMatch(this.txtEmail.Text))
        {
            this.lblErrorEmail.Text = "Invalid email format";
            hasErrors = true;
        }

        if (!phoneRegex.IsMatch(this.txtPhoneNumber.Text))
        {
            this.lblErrorPhone.Text = "Must be 10 digits long. No [ - ] in between";
            hasErrors = true;
        }

        return hasErrors;
    }

    #endregion
}