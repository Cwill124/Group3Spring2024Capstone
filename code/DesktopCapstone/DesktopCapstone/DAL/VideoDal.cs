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
    public class VideoDal
    {

        public ExternalVideo getSpecifiedVideo(int id)
        {


            var connectionString = Connection.ConnectionString;
            var query = "select* from capstone.video_link where id = 1";

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                //var connection = new NpgsqlConnection(connectionString);
                //connection.Open();

                var received = new ExternalVideo();
                //select* from capstone.app_user where username = @Username and password = @Password
                var result = dbConnection.QuerySingleOrDefault<ExternalVideo>(query, received);

                return result;
            }
        }

    }
}
