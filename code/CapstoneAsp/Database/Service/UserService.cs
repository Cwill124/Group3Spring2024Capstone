using CapstoneASP.Database.Repository;
using CapstoneASP.Model;

namespace CapstoneASP.Database.Service;

public interface IUserService
{
    #region Methods

    public Task<User> GetUserByUsername(User user);

    #endregion
}

public class UserService : IUserService
{
    #region Data members

    #region properties

    private readonly IUserRepository repository;

    #endregion

    #endregion

    #region Constructors

    public UserService(IUserRepository repository)
    {
        this.repository = repository;
    }

    #endregion

    #region Methods

    public async Task<User> GetUserByUsername(User user)
    {
        var result = await this.repository.GetUserByUsername(user);

        return result;
    }

    #endregion
}