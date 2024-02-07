using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;

namespace CapstoneASP.Database.Repository;

public interface ILoginRepository
{
    #region Methods

    public Task<UserLogin> GetUserLogin(UserLogin user);
    public Task CreateAccount(UserLogin user);

    #endregion
}

public class LoginRepository : ILoginRepository
{
    #region Data members

    private readonly DBContext.DBContext context;

    #endregion

    #region Constructors

    public LoginRepository(DBContext.DBContext context)
    {
        this.context = context;
    }

    #endregion

    #region Methods

    public async Task<UserLogin> GetUserLogin(UserLogin user)
    {
        using var connection = this.context.Connection;

        connection.Open();
        var foundUser = await connection.QueryAsync<UserLogin>(SqlConstants.GetUserLogin, user);
        return foundUser.ElementAt(0);
    }

    public async Task CreateAccount(UserLogin user)
    {
        using var connection = this.context.Connection;
        connection.Open();
        await connection.ExecuteAsync(SqlConstants.CreateUserLogin, user);
    }

    #endregion
}