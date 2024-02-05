using desktop_capstone.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace desktop_capstone.view
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Login pressed");

            var validLogin = this.handleLogin();

            if (validLogin != null)
            {
                MediaViewer newPage = new MediaViewer();
                //this.Close();
                newPage.Show();
                this.Close();
            }
        }

        private model.AppUser handleLogin()
        {
            var username = this.txtUsername.Text;
            var password = this.txtPassword.Password;

            var loginResult = new model.AppUser();

            try
            {
                loginResult = new LoginDAL().checkLogin(username, password);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("connection failed");
            }
            return loginResult;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
