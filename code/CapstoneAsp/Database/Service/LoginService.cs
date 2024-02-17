using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using CapstoneASP.Util;
using Microsoft.AspNetCore.Identity;

namespace CapstoneASP.Database.Service;

/// <summary>
///     Represents a service interface for login-related operations.
/// </summary>
public interface ILoginService
{
    #region Methods

    /// <summary>
    ///     Retrieves user login information based on the provided user credentials.
    /// </summary>
    /// <param name="user">The user credentials to search for.</param>
    /// <returns>
    ///     A <see cref="Task" /> representing the asynchronous operation, containing the retrieved UserLogin object or
    ///     null if not found.
    /// </returns>
    Task<UserLogin> GetUserLogin(UserLogin user);

    /// <summary>
    ///     Creates a new user account with the specified user credentials.
    /// </summary>
    /// <param name="user">The user credentials for the new account.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task CreateAccount(User user);

    #endregion
}

/// <summary>
///     Represents a service implementation for login-related operations.
/// </summary>
public class LoginService : ILoginService
{
    #region Data members

    private readonly ILoginRepository repository;
    private readonly IUserRepository userRepository;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="LoginService" /> class with the specified repositories.
    /// </summary>
    /// <param name="loginRepository">The repository for login-related operations.</param>
    /// <param name="userRepository">The repository for user-related operations.</param>
    public LoginService(ILoginRepository loginRepository, IUserRepository userRepository)
    {
        this.repository = loginRepository;
        this.userRepository = userRepository;
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public async Task<UserLogin> GetUserLogin(UserLogin user)
    {
        var foundUser = await this.repository.GetUserLogin(user);

        if (PasswordHasher.CheckHashedPassword(foundUser.Password, user.Password) == PasswordVerificationResult.Success)
        {
            return foundUser;
        }

        return null;
    }

    /// <inheritdoc />
    public async Task CreateAccount(User user)
    {
        var userLogin = new UserLogin
        {
            Password = PasswordHasher.HashPassword(user.Password),
            UserId = null,
            Username = user.Username
        };

        await this.repository.CreateAccount(userLogin);

        await this.userRepository.CreateUser(user);
    }

    #endregion
}