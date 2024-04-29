using System.Collections.ObjectModel;
using System.Data;
using Dapper;
using DesktopCapstone.model;
using DesktopCapstone.util;

namespace DesktopCapstone.DAL;

/// <summary>
///     Data Access Layer for managing Tags in the database.
/// </summary>
public class TagDAL
{
    #region Data members

    private readonly IDbConnection dbConnection;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="TagDAL" /> class.
    /// </summary>
    /// <param name="connection">The database connection to be used by the data access layer.</param>
    public TagDAL(IDbConnection connection)
    {
        // Enable Dapper to match property names with underscores in database columns
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        this.dbConnection = connection;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Creates a new tag in the database.
    /// </summary>
    /// <param name="tag">The tag object to be created.</param>
    /// <returns>The number of affected rows in the database.</returns>
    public int CreateTag(Tags tag)
    {
        if (tag.Tag != null)
        {
            this.dbConnection.Open();
            var result = this.dbConnection.Execute(SqlConstants.CreateTag, tag);
            this.dbConnection.Close();
            return result;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    ///     Deletes a tag from the database.
    /// </summary>
    /// <param name="tag">The tag object to be deleted.</param>
    /// <returns>The number of affected rows in the database.</returns>
    public int DeleteTag(Tags tag)
    {
        this.dbConnection.Open();
        var result = this.dbConnection.Execute(SqlConstants.DeleteTag, tag);
        this.dbConnection.Close();
        return result;
    }

    /// <summary>
    ///     Retrieves a collection of tags belonging to a specific user from the database.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <returns>An ObservableCollection of Tags belonging to the specified user.</returns>
    public ObservableCollection<Tags> GetTagsBelongingToUser(string username)
    {
        var tags = new ObservableCollection<Tags>();
        this.dbConnection.Open();
        var result = this.dbConnection.Query<Tags>(SqlConstants.GetTagsBelongingToUser, new { username });
        foreach (var item in result.ToList())
        {
            tags.Add(item);
        }

        this.dbConnection.Close();
        return tags;
    }

    #endregion
}