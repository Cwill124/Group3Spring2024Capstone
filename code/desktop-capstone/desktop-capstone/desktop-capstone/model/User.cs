using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_capstone.model
{
    public class User
    {

        private string username;
        private string password;

        public string UserName { get { return username; }  }
        public string Password { get { return password; }  }

        public User() { 
            this.username = string.Empty;
            this.password = string.Empty;
        }

        public User(string username, string password) {
            this.username = username;
            this.password = password;
        }


    }
}
