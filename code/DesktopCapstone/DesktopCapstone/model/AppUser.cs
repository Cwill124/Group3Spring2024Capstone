using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_capstone.model
{
    public class AppUser
    {

        private string username;
        private string firstName;
        private string lastName;
        private string email;
        private string phoneNumber;
       

        public string Username { get { return username; }  }
        public string FirstName { get { return firstName; } }
        public string LastName { get { return lastName; } }
        public string Email { get { return email; } }
        public string PhoneNumber { get { return phoneNumber; } }



        public AppUser() { 
            this.username = string.Empty;
            this.firstName = string.Empty;
            this.lastName = string.Empty;
            this.email = string.Empty;
            this.phoneNumber = string.Empty;
        }

        public AppUser(string username, string firstName, string lastName, string email, string phoneNumber) {
            this.username = username;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
        }


    }
}
