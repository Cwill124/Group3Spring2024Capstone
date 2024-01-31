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
using Npgsql;

namespace desktop_capstone.DAL
{
    public class LoginDAL
    {
        public model.User checkLogin(string username, string password)
        {


            var connectionString = Connection.ConnectionString;
            var query = "select* from capstone.app_user where username = @Username and password = @Password";

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                

                //var received = new model.User(username, password);
                //select* from capstone.app_user where username = @Username and password = @Password
                var result =  dbConnection.QuerySingleOrDefault<model.User>(query);
                
                return result;
            }
            //return foundUser.ToList().ElementAt(0);
        }
    }
}
