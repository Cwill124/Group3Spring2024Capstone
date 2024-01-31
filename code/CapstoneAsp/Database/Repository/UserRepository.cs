using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;

namespace CapstoneASP.Database.Repository
{
    public interface IUserRepository
    {
        public Task CreateUser(User user);

        public Task<User> GetUserByUsername(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly DBContext.DBContext context;

        public UserRepository(DBContext.DBContext context)
        {
            this.context = context;
        }

        public async Task CreateUser(User user)
        {
            using var connection = context.Connection;

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
    }
}
