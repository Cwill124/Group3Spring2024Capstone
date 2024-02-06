using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;

namespace CapstoneASP.Database.Repository;

public interface IUserRepository
{
    #region Methods

    public Task CreateUser(User user);

    public Task<User> GetUserByUsername(User user);

    #endregion
}

public class UserRepository : IUserRepository
{
    #region Data members

    private readonly DBContext.DBContext context;

    #endregion

    #region Constructors

    public UserRepository(DBContext.DBContext context)
    {
        this.context = context;
    }

    #endregion

    #region Methods

    public async Task CreateUser(User user)
    {
        using var connection = this.context.Connection;

        connection.Open();

        await connection.ExecuteAsync(SqlConstants.CreateUser, user);
    }

    public async Task<User> GetUserByUsername(User user)
    {
        using var connection = this.context.Connection;

        connection.Open();

        var result = await connection.QueryFirstOrDefaultAsync<User>(SqlConstants.GetUserByUsername, user);

        return result!;
    }

    #endregion
}