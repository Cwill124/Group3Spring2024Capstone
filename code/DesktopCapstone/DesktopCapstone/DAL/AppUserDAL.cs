using Dapper;
using desktop_capstone.model;
using DesktopCapstone.model;
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

        public bool createNewUser(string username, string password, string firstName, string lastName, string email, string phoneNumber)
        {
            var connectionString = Connection.ConnectionString;
            var userToAdd = new AppUser(username, firstName, lastName, email, phoneNumber);
            var query = "insert into capstone.app_user values (@Username, @FirstName, @LastName, @PhoneNumber, @Email)";
            var result = false;
            var rowsEffected = 0;
            this.createNewLoginInfo(username, password);

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                rowsEffected = dbConnection.Execute(query, userToAdd);
                
            }

            if (rowsEffected > 0)
            {
                result = true;
            }
            return result;
        }

        private void createNewLoginInfo(string username, string password)
        {
            var hashedPassword = PasswordHasher.HashPassword(password);
            var connectionString = Connection.ConnectionString;
            var loginToAdd = new LoginInfo(username, hashedPassword);
            var query = "insert into capstone.login (username, password) values (@Username, @Password)";
            
            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                dbConnection.Execute(query, loginToAdd);
            }
        }        
    }
}
