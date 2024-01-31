using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;

namespace CapstoneASP.Database.Repository
{
    public interface IUserRepository
    {
        public Task CreateUser(User user);

        public Task<User> GetUserByUsername(string username);
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

        public Task<User> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
