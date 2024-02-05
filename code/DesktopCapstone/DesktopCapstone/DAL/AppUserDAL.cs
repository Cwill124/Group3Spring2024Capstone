using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_capstone.DAL
{
    public class AppUserDAL
    {

        public void createNewUser(string username, string password)
        {
            var connectionString = Connection.ConnectionString;
            var userToAdd = new model.User(username, password, "Jim", "Raynor", "xd@lamo.com", "770", "user");
            var query = "insert into capstone.app_user (username, password, firstname, lastname, email, phone, role) values (@username, @password, @fName, @lName, @email, @phoneNumber, @role)";

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {


                
            }
        }
      
        public 
    }
}
