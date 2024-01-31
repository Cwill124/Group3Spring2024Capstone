using desktop_capstone.model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;


namespace desktop_capstone.DAL
{
    public class DocumentDAL
    {
        public ExternalDocument getSpecifiedDocument(int intId)
        {


            var connectionString = Connection.ConnectionString;
            var id = intId.ToString();
            var query = "select* from capstone.pdf where id = 1";

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                //var connection = new NpgsqlConnection(connectionString);
                //connection.Open();

                var received = new ExternalDocument();
                //select* from capstone.app_user where username = @Username and password = @Password
                var result = dbConnection.QuerySingleOrDefault<ExternalDocument>(query, received);
                
                return result;
            }
            //return foundUser.ToList().ElementAt(0);
        }
    }
}
