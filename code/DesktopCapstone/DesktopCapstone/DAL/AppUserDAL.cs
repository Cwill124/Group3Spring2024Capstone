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
    /// <summary>
    /// Data Access Layer for handling operations related to AppUser entities.
    /// </summary>
    public class AppUserDAL
    {
        /// <summary>
        /// Creates a new user with the provided information.
        /// </summary>
        /// <param name="username">The username of the new user.</param>
        /// <param name="password">The password of the new user.</param>
        /// <param name="firstName">The first name of the new user.</param>
        /// <param name="lastName">The last name of the new user.</param>
        /// <param name="email">The email of the new user.</param>
        /// <param name="phoneNumber">The phone number of the new user.</param>
        /// <returns>True if the user creation is successful; otherwise, false.</returns>
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

        /// <summary>
        /// Creates new login information for the user with the specified username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
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
