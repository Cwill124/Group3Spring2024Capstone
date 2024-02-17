using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_capstone.model
{
    public class AppUser
    {

        //private string username;
        //private string firstName;
        //private string lastName;
        //private string email;
        //private string phoneNumber;
       

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }



        public AppUser() { 
            this.Username = string.Empty;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Email = string.Empty;
            this.PhoneNumber = string.Empty;
        }

        public AppUser(string username, string firstName, string lastName, string email, string phoneNumber) {
            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }


    }
}
