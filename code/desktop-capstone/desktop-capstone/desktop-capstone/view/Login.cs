using desktop_capstone.DAL;
using desktop_capstone.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace desktop_capstone
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Login pressed");
            var username = this.txtUsername.Text;
            var password = this.txtPassword.Text;

            var loginResult = new LoginDAL().checkLogin(username, password);
            
            if (loginResult)
            {
                MediaViewer newPage = new MediaViewer();
                //this.Close();
                newPage.Show();
                //this.Close();
            }
        }
    }
}
