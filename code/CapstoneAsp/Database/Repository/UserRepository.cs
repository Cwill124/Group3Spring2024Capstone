using CapstoneASP.Database.DBContext;
using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;

namespace CapstoneASP.Database.Repository;

/// <summary>
///     Represents a repository interface for user-related operations.
/// </summary>
public interface IUserRepository
{
    #region Methods

    /// <summary>
    ///     Creates a new user with the specified details.
    /// </summary>
    /// <param name="user">The user details to be created.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task CreateUser(User user);

    /// <summary>
    ///     Retrieves user information based on the provided username.
    /// </summary>
    /// <param name="user">The user object containing the username to search for.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation, containing the retrieved User object.</returns>
    Task<User> GetUserByUsername(User user);

    #endregion
}

/// <summary>
///     Represents a repository implementation for user-related operations.
/// </summary>
public class UserRepository : IUserRepository
{
    #region Data members

    private readonly IDataContext context;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserRepository" /> class with the specified database context.
    /// </summary>
    /// <param name="context">The database context used for repository operations.</param>
    public UserRepository(IDataContext context)
    {
        this.context = context;
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public async Task CreateUser(User user)
    {
        using var connection = await this.context.CreateConnection();

        connection.Open();

        await connection.ExecuteAsync(SqlConstants.CreateUser, user);
    }

    /// <inheritdoc />
    public async Task<User> GetUserByUsername(User user)
    {
        using var connection = await this.context.CreateConnection();

        connection.Open();

        var result = await connection.QueryAsync<User>(SqlConstants.GetUserByUsername, user);

        return result.ElementAt(0);
    }

    #endregion
}