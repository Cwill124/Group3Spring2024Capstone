using Npgsql;
using System.Data;
using Dapper;
using DesktopCapstone.model;
using System.Collections.ObjectModel;
using DesktopCapstone.util;

namespace desktop_capstone.DAL
{
    /// <summary>
    /// Data Access Layer for handling operations related to Source entities.
    /// </summary>
    public class SourceDAL
    {
        /// <summary>
        /// Retrieves all sources available in the system.
        /// </summary>
        /// <returns>An ObservableCollection of Source objects.</returns>
        public ObservableCollection<Source> GetAllSources()
        {
            var connectionString = Connection.ConnectionString;

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                var sourceList = new List<dynamic>(dbConnection.Query<dynamic>(SqlConstants.GetAllSources).ToList());
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

        /// <summary>
        /// Retrieves a source by its ID.
        /// </summary>
        /// <param name="id">The ID of the source to retrieve.</param>
        /// <returns>A Source object if found; otherwise, null.</returns>
        public Source GetSourceWithId(int id)
        {
            var connectionString = Connection.ConnectionString;

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                var source = dbConnection.QueryFirstOrDefault<Source>(SqlConstants.GetSourceById, new { id });
                return source;
            }
        }

        /// <summary>
        /// Retrieves a source by its name.
        /// </summary>
        /// <param name="name">The name of the source to retrieve.</param>
        /// <returns>A Source object if found; otherwise, null.</returns>
        public Source getSourceWithName(string name)
        {
            var connectionString = Connection.ConnectionString;
            var query = "select * from capstone.source where name = @Name";

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                var source = dbConnection.QueryFirstOrDefault<Source>(query, new { Name = name });
                return source;
            }
        }

        /// <summary>
        /// Retrieves all source types available in the system.
        /// </summary>
        /// <returns>An ObservableCollection of SourceType objects.</returns>
        public ObservableCollection<SourceType> GetSourceTypes()
        {
            using var connection = new NpgsqlConnection(Connection.ConnectionString);

            var sourceTypes = new ObservableCollection<SourceType>();

            var result = connection.QueryAsync<dynamic>(SqlConstants.GetSourceTypes);

            foreach (var item in result.Result.ToList())
            {
                var newSourceType = new SourceType
                {
                    SourceTypeId = item.source_type_id,
                    TypeName = item.type_name
                };
                sourceTypes.Add(newSourceType);
            }
            return sourceTypes;
        }

        /// <summary>
        /// Creates a new source with the provided information.
        /// </summary>
        /// <param name="sourceToAdd">The Source object containing source details.</param>
        /// <returns>True if the source creation is successful; otherwise, false.</returns>
        public bool CreateSource(Source sourceToAdd)
        {
            var connectionString = Connection.ConnectionString;
            var result = false;
            var rowsEffected = 0;

            using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        rowsEffected = dbConnection.Execute(SqlConstants.CreateSource, sourceToAdd, transaction);
                        transaction.Commit();
                    }
                    catch (Exception e)
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

        /// <summary>
        /// Deletes a source by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the source to be deleted.</param>
        public async void DeleteById(int id)
        {
            await using var connection = new NpgsqlConnection(Connection.ConnectionString);

            await connection.OpenAsync();

            await connection.ExecuteAsync(SqlConstants.DeleteSourceById, new { id });
        }
    }
}
