using CapstoneASP.Database.DBContext;
using CapstoneASP.Model;
using CapstoneASP.Util;

namespace CapstoneASP.Database.Repository;

/// <summary>
///     Represents a repository interface for user login-related operations.
/// </summary>
public interface ILoginRepository
{
    #region Methods

    /// <summary>
    ///     Retrieves user login information based on the provided user credentials.
    /// </summary>
    /// <param name="user">The user credentials to search for.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation, containing the retrieved UserLogin object.</returns>
    Task<UserLogin> GetUserLogin(UserLogin user);

    /// <summary>
    ///     Creates a new user account with the specified user credentials.
    /// </summary>
    /// <param name="user">The user credentials for the new account.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task CreateAccount(UserLogin user);

    Task CheckForAccountExisting(UserLogin user);

    #endregion
}

/// <summary>
///     Represents a repository implementation for user login-related operations.
/// </summary>
public class LoginRepository : ILoginRepository
{
    #region Data members

    private readonly IDataContext context;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="LoginRepository" /> class with the specified database context.
    /// </summary>
    /// <param name="context">The database context used for repository operations.</param>
    public LoginRepository(IDataContext context)
    {
        this.context = context;
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public async Task<UserLogin> GetUserLogin(UserLogin user)
    {
        using var connection = await this.context.CreateConnection();

        var foundUser = await connection.QueryAsync<UserLogin>(SqlConstants.GetUserLogin, user);
        return foundUser.ElementAt(0);
    }

    /// <inheritdoc />
    public async Task CreateAccount(UserLogin user)
    {
        using var connection = await this.context.CreateConnection();

        await connection.ExecuteAsync(SqlConstants.CreateUserLogin, user);
    }

    public async Task CheckForAccountExisting(UserLogin user)
    {
        using var connection = await this.context.CreateConnection();

        var result = await connection.QueryAsync<UserLogin>(SqlConstants.CheckForExistingUser, user);

        if (result.Count() > 0)
        {
            throw new ArgumentException("User already Exists");
        }
    }

    #endregion
}