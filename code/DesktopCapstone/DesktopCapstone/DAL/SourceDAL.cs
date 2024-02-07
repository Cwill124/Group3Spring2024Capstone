using desktop_capstone.model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DesktopCapstone.model;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text.Json.Nodes;


namespace desktop_capstone.DAL
{
    public class SourceDAL
    {
        public ObservableCollection<Source> getAllSources()
        {
            var connectionString = Connection.ConnectionString;
            var query = "select * from capstone.source";
            //var sourceList = new ObservableCollection<Source>();

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                var sourceList = new ObservableCollection<Source>(dbConnection.Query<Source>(query).ToList());
                return sourceList;
            }
            //return null;
        }

        public ObservableCollection<SourceType> getSourceTypes()
        {
            var connectionString = Connection.ConnectionString;
            var query = "select * from capstone.source_type";
            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                var sourceTypeList = new ObservableCollection<SourceType>(dbConnection.Query<SourceType>(query).ToList());
                return sourceTypeList;
            }
        }

        public bool addNewSource(Source sourceToAdd)
        {
            var connectionString = Connection.ConnectionString;
            var query = "insert into capstone.source values (description, name, content, meta_data, source_type_id, tags, created_by) values (@Description, @Name, @Content, @MetaData, @SourceType, @Tags, @CreatedBy)";
            //var sourceToAdd = new Source(description, name, content, metaData, sourceType, tags, createdBy);
            var result = false;
            var rowsEffected = 0;

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                rowsEffected = dbConnection.Execute(query, sourceToAdd);
            }

            if (rowsEffected > 0)
            {
                result = true;
            }
            return result;

        }



        public Source getSpecifiedSource(int intId)
        {


            var connectionString = Connection.ConnectionString;
            var id = intId.ToString();
            var query = "select* from capstone.pdf where id = 1";

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                //var connection = new NpgsqlConnection(connectionString);
                //connection.Open();

                //var received = new ExternalDocument();
                //select* from capstone.app_user where username = @Username and password = @Password
                //var result = dbConnection.QuerySingleOrDefault<ExternalDocument>(query, received);
                
                //return result;
            }
            return null;
            //return foundUser.ToList().ElementAt(0);
        }
    }
}
