using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using desktop_capstone.model;
using DesktopCapstone.model;
using Microsoft.AspNetCore.Identity;
using Npgsql;

namespace desktop_capstone.DAL
{
    public class LoginDAL
    {

        public AppUser checkLogin(string username, string password)
        {


            var connectionString = Connection.ConnectionString;
            var query = "select* from capstone.login where username = @username";
            AppUser userToReturn = null;
            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                

                var result =  dbConnection.QuerySingleOrDefault<LoginInfo>(query, new {username = username});
                var passwordChecked = Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed;
                if (result != null)
                {
                    passwordChecked = PasswordHasher.CheckHashedPassword(result.Password, password);
                }
                if (passwordChecked.Equals(PasswordVerificationResult.Success)) {
                    userToReturn = this.getUserFromLogin(username, dbConnection);
                }

                return userToReturn;
            }
 
        }

        private AppUser getUserFromLogin(string username, IDbConnection connection)
        {
            var query = "select* from capstone.app_user where username = @username";
            var result = connection.QueryFirstOrDefault<AppUser>(query, new { username = username});
            return result;


        }
    }
}
