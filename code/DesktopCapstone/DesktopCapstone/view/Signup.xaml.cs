using desktop_capstone.DAL;
using desktop_capstone.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            AppUserDAL dal = new AppUserDAL();
            var creationSuccess = dal.createNewUser(txtUsername.Text, txtPassword.Text, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPhoneNumber.Text);
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
    }
}
