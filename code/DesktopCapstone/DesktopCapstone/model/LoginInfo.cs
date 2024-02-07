using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCapstone.model
{
    public class LoginInfo
    {
        private string username;
        private string password;

        public string Username { get { return this.username; } }
        public string Password { get { return this.password; } }

        public LoginInfo()
        {
            this.username = string.Empty;
            this.password = string.Empty;
        }

        public LoginInfo(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
