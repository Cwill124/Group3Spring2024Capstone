using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using CapstoneASP.Util;
using Microsoft.AspNetCore.Identity;

namespace CapstoneASP.Database.Service;

public interface ILoginService
{
    #region Methods

    public Task<UserLogin> GetUserLogin(UserLogin user);
    public Task CreateAccount(UserLogin user);

    #endregion
}

public class LoginService : ILoginService
{
    #region Data members

    private readonly ILoginRepository repository;

    private readonly IUserRepository userRepository;

    #endregion

    #region Constructors

    public LoginService(ILoginRepository loginRepository, IUserRepository userRepository)
    {
        this.repository = loginRepository;
        this.userRepository = userRepository;
    }

    #endregion

    #region Methods

    public async Task<UserLogin> GetUserLogin(UserLogin user)
    {
        var foundUser = await this.repository.GetUserLogin(user);

        if (PasswordHasher.CheckHashedPassword(foundUser.Password, user.Password) == PasswordVerificationResult.Success)
        {
            return foundUser;
        }

        return null;
    }

    public async Task CreateAccount(UserLogin user)
    {
        user.Password = PasswordHasher.HashPassword(user.Password);
        await this.repository.CreateAccount(user);
        var newUser = new User
        {
            Username = user.Username
        };
        await this.userRepository.CreateUser(newUser);
    }

    #endregion
}