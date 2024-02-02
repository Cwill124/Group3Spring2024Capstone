using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;
using Microsoft.AspNetCore.Components.Forms;

namespace CapstoneASP.Database.Repository
{
    public interface ILoginRepository
    {
        public Task<UserLogin> GetUserLogin(UserLogin user);
        public Task CreateAccount(UserLogin user);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly DBContext.DBContext context;

        public LoginRepository(DBContext.DBContext context)
        {
            this.context = context;
        }
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
    }
}
