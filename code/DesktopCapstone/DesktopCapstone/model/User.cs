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
        private string firstName;
        private string lastName;
        private string email;
        private string phoneNumber;
        private string role;

        public string Username { get { return username; }  }
        public string Password { get { return password; }  }
        public string FirstName { get { return firstName; } }
        public string LastName { get { return lastName; } }
        public string Email { get { return email; } }
        public string PhoneNumber { get { return phoneNumber; } }
        public string Role { get { return role; } }


        public User() { 
            this.username = string.Empty;
            this.password = string.Empty;
            this.firstName = string.Empty;
            this.lastName = string.Empty;
            this.email = string.Empty;
            this.phoneNumber = string.Empty;
            this.role = string.Empty;
        }

        public User(string username, string password, string firstName, string lastName, string email, string phoneNumber, string role) {
            this.username = username;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.role = role;
        }


    }
}
