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
using DesktopCapstone.util;

namespace desktop_capstone.DAL
{
    public class AppUserDAL
    {

        public bool CreateNewUser(string username, string password, string firstName, string lastName, string email, string phoneNumber)
        {
            var connectionString = Connection.ConnectionString;
            var userToAdd = new AppUser(username, firstName, lastName, email, phoneNumber);
            var result = false;
            var rowsEffected = 0;
            this.CreateNewLoginInfo(username, password);

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                rowsEffected = dbConnection.Execute(SqlConstants.CreateAppUser, userToAdd);
                
            }

            if (rowsEffected > 0)
            {
                result = true;
            }
            return result;
        }

        private void CreateNewLoginInfo(string username, string password)
        {
            var hashedPassword = PasswordHasher.HashPassword(password);
            var connectionString = Connection.ConnectionString;
            var loginToAdd = new LoginInfo(username, hashedPassword);
            
            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                dbConnection.Execute(SqlConstants.CreateLoginInfo, loginToAdd);
            }
        }        
    }
}
