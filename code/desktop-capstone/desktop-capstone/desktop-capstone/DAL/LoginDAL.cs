using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace desktop_capstone.DAL
{
    public class LoginDAL
    {
        public bool checkLogin(string username, string password)
        {
            var connectionString = Connection.ConnectionString;
            var query = "select userId from `User` where username = @username and password = @password";

            //using var command = new MySqlCommand(query, connectionString);

            //command.Parameters.AddWithValue("@username", username);
            //command.Parameters.AddWithValue("@password", password);
            //var result = command.ExecuteScalar();
            var result = 0;
            
            return result == 0;
        }
    }
}
