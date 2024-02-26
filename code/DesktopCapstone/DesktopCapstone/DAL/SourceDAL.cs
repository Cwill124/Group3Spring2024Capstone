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

        private IDbConnection dbConnection;

        public SourceDAL(IDbConnection connection)
        {
            this.dbConnection = connection;
        }

        /// <summary>
        /// Retrieves all sources available in the system.
        /// </summary>
        /// <returns>An ObservableCollection of Source objects.</returns>
        public ObservableCollection<Source> GetAllSources()
        {
            var connectionString = Connection.ConnectionString;

            this.dbConnection.Open();
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
            this.dbConnection.Close();
            return sourceListToReturn;

        }

        /// <summary>
        /// Retrieves a source by its ID.
        /// </summary>
        /// <param name="id">The ID of the source to retrieve.</param>
        /// <returns>A Source object if found; otherwise, null.</returns>
        public Source GetSourceWithId(int id)
        {
            var connectionString = Connection.ConnectionString;

            this.dbConnection.Open();
            var source = dbConnection.QueryFirstOrDefault<Source>(SqlConstants.GetSourceById, new { id });
            this.dbConnection.Close();
            return source;

        }


        /// <summary>
        /// Retrieves all source types available in the system.
        /// </summary>
        /// <returns>An ObservableCollection of SourceType objects.</returns>
        public ObservableCollection<SourceType> GetSourceTypes()
        {
            //var connection = this.dbConnection;

            var sourceTypes = new ObservableCollection<SourceType>();

            this.dbConnection.Open();
            var result = this.dbConnection.Query<dynamic>(SqlConstants.GetSourceTypes).ToList();

            foreach (var item in result)
            {
                var newSourceType = new SourceType
                {
                    SourceTypeId = item.source_type_id,
                    TypeName = item.type_name
                };
                sourceTypes.Add(newSourceType);
            }
            this.dbConnection.Close();
            return sourceTypes;
        }

        /// <summary>
        /// Creates a new source with the provided information.
        /// </summary>
        /// <param name="sourceToAdd">The Source object containing source details.</param>
        /// <returns>True if the source creation is successful; otherwise, false.</returns>
        public bool CreateSource(Source sourceToAdd)
        {
            var result = false;
            var rowsEffected = 0;

            this.dbConnection.Open();
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


            if (rowsEffected > 0)
            {
                result = true;
            }
            this.dbConnection.Close();
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
