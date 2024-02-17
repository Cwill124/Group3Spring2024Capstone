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

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                var sourceList = new List<dynamic>(dbConnection.Query<dynamic>(query).ToList());
                var sourceListToReturn = new ObservableCollection<Source>();
                foreach (var source in sourceList)
                {
                    var sourceToAdd = new Source
                    {
                        SourceId = source.source_id,
                        Description = source.description,
                        Name = source.name,
                        Content = source.content,
                        MetaData = source.meta_data,
                        SourceType = source.source_type_id,
                        Tags = source.tags,
                        CreatedBy = source.created_by
                    };
                    sourceListToReturn.Add(sourceToAdd);
                }   

                return sourceListToReturn;
            }
        }

        public Source getSourceWithId(int id)
        {
            var connectionString = Connection.ConnectionString;
            var query = "select * from capstone.source where source_id = @id";

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                var source = dbConnection.QueryFirstOrDefault<Source>(query, new { id });
                return source;
            }
        }

        public Source getSourceWithName(string name)
        {
            var connectionString = Connection.ConnectionString;
            var query = "select * from capstone.source where name = @Name";;

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                var source = dbConnection.QueryFirstOrDefault<Source>(query, new { Name = name });
                return source;
            }
        }

        public ObservableCollection<SourceType> getSourceTypes()
        {
            var connectionString = Connection.ConnectionString;
            var query = "select * from capstone.source_type";
            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                var result = dbConnection.Query<SourceType>(query).ToList();
                var sourceTypeList = new ObservableCollection<SourceType>(result);
                return sourceTypeList;
            }
        }

        public bool addNewSource(Source sourceToAdd)
        {
            var connectionString = Connection.ConnectionString;
            var query = "insert into capstone.source (description, name, content, meta_data, source_type_id, tags, created_by) values (@Description, @Name, @Content::json, @MetaData::json, @SourceType, @Tags::json, @CreatedBy)";           
            var result = false;
            var rowsEffected = 0;

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        rowsEffected = dbConnection.Execute(query, sourceToAdd, transaction);
                        transaction.Commit();
                    } catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                } 
            }

            if (rowsEffected > 0)
            {
                result = true;
            }
            return result;

        }

      
    }
}
