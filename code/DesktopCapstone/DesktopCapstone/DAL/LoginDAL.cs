using System.Data;
using Dapper;
using DesktopCapstone.model;
using DesktopCapstone.util;
using Microsoft.AspNetCore.Identity;

namespace DesktopCapstone.DAL;

/// <summary>
///     Data Access Layer for handling login-related operations.
/// </summary>
public class LoginDAL
{
    #region Data members

    private readonly IDbConnection dbConnection;

    #endregion

    #region Constructors

    public LoginDAL(IDbConnection connection)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        this.dbConnection = connection;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Checks the login credentials for a user.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>Returns an AppUser if login is successful; otherwise, returns null.</returns>
    public AppUser checkLogin(string username, string password)
    {
        var connectionString = Connection.ConnectionString;
        AppUser userToReturn = null;

        var result = this.dbConnection.QuerySingleOrDefault<LoginInfo>(SqlConstants.CheckLogin, new { username });
        var passwordChecked = PasswordVerificationResult.Failed;

        if (result != null)
        {
            passwordChecked = PasswordHasher.CheckHashedPassword(result.Password, password);
        }

        if (passwordChecked.Equals(PasswordVerificationResult.Success))
        {
            userToReturn = this.getUserFromLogin(username, this.dbConnection);
        }

        return userToReturn;
    }

    /// <summary>
    ///     Checks if the specified username is already in use.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns>True if the username is in use; otherwise, false.</returns>
    public bool checkIfUsernameIsInUse(string username)
    {
        var usernameInUse = false;
        var connectionString = Connection.ConnectionString;

        var result =
            this.dbConnection.QuerySingleOrDefault<LoginInfo>(SqlConstants.CheckIfUsernameInUse, new { username });

        if (result != null)
        {
            usernameInUse = true;
        }

        return usernameInUse;
    }

    /// <summary>
    ///     Gets the AppUser information based on the username.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="connection">The database connection.</param>
    /// <returns>Returns an AppUser if found; otherwise, returns null.</returns>
    private AppUser getUserFromLogin(string username, IDbConnection connection)
    {
        var result = connection.QueryFirstOrDefault<AppUser>(SqlConstants.GetAppUserByUsername, new { username });
        return result;
    }

    #endregion
}