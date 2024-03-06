using System.Data;
using Dapper;
using DesktopCapstone.model;
using DesktopCapstone.util;

namespace DesktopCapstone.DAL;

/// <summary>
///     Data Access Layer for handling operations related to AppUser entities.
/// </summary>
public class AppUserDAL
{
    #region Data members

    private readonly IDbConnection dbConnection;

    #endregion

    #region Constructors

    public AppUserDAL(IDbConnection connection)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        this.dbConnection = connection;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Creates a new user with the provided information.
    /// </summary>
    /// <param name="username">The username of the new user.</param>
    /// <param name="password">The password of the new user.</param>
    /// <param name="firstName">The first name of the new user.</param>
    /// <param name="lastName">The last name of the new user.</param>
    /// <param name="email">The email of the new user.</param>
    /// <param name="phoneNumber">The phone number of the new user.</param>
    /// <returns>True if the user creation is successful; otherwise, false.</returns>
    public bool CreateNewUser(string username, string password, string firstName, string lastName, string email,
        string phoneNumber)
    {
        var connectionString = Connection.ConnectionString;
        var userToAdd = new AppUser(username, firstName, lastName, email, phoneNumber);
        var result = false;
        var rowsEffected = 0;
        this.CreateNewLoginInfo(username, password);

        rowsEffected = this.dbConnection.Execute(SqlConstants.CreateAppUser, userToAdd);

        if (rowsEffected > 0)
        {
            result = true;
        }

        return result;
    }

    /// <summary>
    ///     Creates new login information for the user with the specified username.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="password">The password of the user.</param>
    private void CreateNewLoginInfo(string username, string password)
    {
        var hashedPassword = PasswordHasher.HashPassword(password);
        var connectionString = Connection.ConnectionString;
        var loginToAdd = new LoginInfo(username, hashedPassword);

        this.dbConnection.Execute(SqlConstants.CreateLoginInfo, loginToAdd);
    }

    #endregion
}