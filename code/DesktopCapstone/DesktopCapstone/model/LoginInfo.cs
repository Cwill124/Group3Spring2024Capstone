using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCapstone.model
{
    public class LoginInfo
    {
        //private string username;
        //private string password;

        public string Username { get; set; }
        public string Password { get; set; }

        public LoginInfo()
        {
            this.Username = string.Empty;
            this.Password = string.Empty;
        }

        public LoginInfo(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
