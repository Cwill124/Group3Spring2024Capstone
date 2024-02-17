using desktop_capstone.DAL;
using desktop_capstone.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopCapstone.view
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.checkForErrors())
            {
                return;
            }
            AppUserDAL dal = new AppUserDAL();
            var creationSuccess = dal.CreateNewUser(txtUsername.Text, txtPassword.Password, txtFirstName.Text,
                txtLastName.Text, txtEmail.Text, txtPhoneNumber.Text);
            if (creationSuccess)
            {
                Login newPage = new Login();
                newPage.Show();
                this.Hide();
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            Login newPage = new Login();
            newPage.Show();
            this.Hide();
        }

        private bool checkForErrors()
        {
            bool hasErrors = false;
            LoginDAL dal = new LoginDAL();
            
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

        //var finalPhoneFormat = new Regex("^\\d{3}-\\d{3}-\\d{4}$|\\d{10}");
    }
}

