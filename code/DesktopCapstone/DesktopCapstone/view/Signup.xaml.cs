using System.Data;
using desktop_capstone.DAL;
using desktop_capstone.view;
using System.Text.RegularExpressions;
using System.Windows;
using Npgsql;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignUp"/> class.
        /// </summary>
        public SignUp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for the "Sign Up" button click.
        /// Checks for input errors, creates a new user, and navigates to the login page upon success.
        /// </summary>
        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.CheckForErrors())
            {
                return;
            }

            AppUserDAL dal = new AppUserDAL(new NpgsqlConnection(Connection.ConnectionString));
            var creationSuccess = dal.CreateNewUser(txtUsername.Text, txtPassword.Password, txtFirstName.Text,
                txtLastName.Text, txtEmail.Text, txtPhoneNumber.Text);

            if (creationSuccess)
            {
                Login newPage = new Login();
                newPage.Show();
                this.Hide();
            }
        }

        /// <summary>
        /// Event handler for the "Return" button click.
        /// Navigates to the login page.
        /// </summary>
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            Login newPage = new Login();
            newPage.Show();
            this.Hide();
        }

        /// <summary>
        /// Checks for input errors and displays corresponding error messages.
        /// </summary>
        /// <returns>True if there are errors, otherwise false.</returns>
        private bool CheckForErrors()
        {
            bool hasErrors = false;
            LoginDAL dal = new LoginDAL(new NpgsqlConnection(Connection.ConnectionString));

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
                this.lblErrorUsername.Text = "Username must be at least 5 characters long and contain only letters, numbers, and underscores";
                hasErrors = true;
            }

            if (dal.checkIfUsernameIsInUse(this.txtUsername.Text))
            {
                this.lblErrorUsername.Text = "Username is already in use";
                hasErrors = true;
            }

            if (!passwordRegex.IsMatch(this.txtPassword.Password))
            {
                this.lblErrorPassword.Text = "Password must be at least 8 characters long and contain at least one uppercase letter and one number";
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
                this.lblErrorPhone.Text = "Phone number must be 10 digits long. No [ - ] in between";
                hasErrors = true;
            }

            return hasErrors;
        }

    }
}
