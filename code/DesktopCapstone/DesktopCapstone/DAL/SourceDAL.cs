using System.Collections.ObjectModel;
using System.Data;
using Dapper;
using DesktopCapstone.model;
using DesktopCapstone.util;
using Npgsql;

namespace DesktopCapstone.DAL;

/// <summary>
///     Data Access Layer for handling operations related to Source entities.
/// </summary>
public class SourceDAL
{
    #region Data members

    private readonly IDbConnection dbConnection;

    #endregion

    #region Constructors

    public SourceDAL(IDbConnection connection)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        this.dbConnection = connection;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Retrieves all sources available in the system.
    /// </summary>
    /// <returns>An ObservableCollection of Source objects.</returns>
    public ObservableCollection<Source> GetAllSourcesByUser(string username)
    {
        var connectionString = Connection.ConnectionString;

        this.dbConnection.Open();
        var sourceList = new List<dynamic>(this.dbConnection.Query<Source>(SqlConstants.GetSourcesByUsername, new {username}).ToList());
        var sourceListToReturn = new ObservableCollection<Source>();
        foreach (var source in sourceList)
        {
            sourceListToReturn.Add(source);
        }

        this.dbConnection.Close();
        return sourceListToReturn;
    }

    /// <summary>
    ///     Retrieves a source by its ID.
    /// </summary>
    /// <param name="id">The ID of the source to retrieve.</param>
    /// <returns>A Source object if found; otherwise, null.</returns>
    public Source GetSourceWithId(int id)
    {
        var connectionString = Connection.ConnectionString;

        this.dbConnection.Open();
        var source = this.dbConnection.QueryFirstOrDefault<Source>(SqlConstants.GetSourceById, new { id });
        this.dbConnection.Close();
        return source;
    }

    /// <summary>
    ///     Retrieves all source types available in the system.
    /// </summary>
    /// <returns>An ObservableCollection of SourceTypeId objects.</returns>
    public ObservableCollection<SourceType> GetSourceTypes()
    {
        //var connection = this.dbConnection;

        var sourceTypes = new ObservableCollection<SourceType>();

        this.dbConnection.Open();
        var result = this.dbConnection.Query<SourceType>(SqlConstants.GetSourceTypes).ToList();

        foreach (var item in result)
        {
            sourceTypes.Add(item);
        }

        this.dbConnection.Close();
        return sourceTypes;
    }

    /// <summary>
    ///     Creates a new source with the provided information.
    /// </summary>
    /// <param name="sourceToAdd">The Source object containing source details.</param>
    /// <returns>The number of affected rows after the source creation.</returns>
    public int CreateSource(Source sourceToAdd)
    {
        var rowsEffected = 0;

        try
        {
            this.dbConnection.Open();
            rowsEffected = this.dbConnection.Execute(SqlConstants.CreateSource, sourceToAdd);
        }
        catch (Exception e)
        {
            // Handle the exception if needed
        }
        finally
        {
            this.dbConnection.Close();
        }

        return rowsEffected;
    }

    /// <summary>
    ///     Deletes a source by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the source to be deleted.</param>
    public async void DeleteById(int id)
    {
        await using var connection = new NpgsqlConnection(Connection.ConnectionString);

        await connection.OpenAsync();

        await connection.ExecuteAsync(SqlConstants.DeleteSourceById, new { id });

        await connection.CloseAsync();
    }

    #endregion
}