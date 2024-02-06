using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;

namespace CapstoneASP.Database.Repository
{
    public interface ISourceRepository
    {
        public Task<IEnumerable<Source>> GetSourcesByUsername(string username);
        public Task Create(Source source);
    }

    public class SourceRepository : ISourceRepository
    {
        private readonly DBContext.DBContext context;

        public SourceRepository(DBContext.DBContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Source>> GetSourcesByUsername(string username)
        {
            using var connection = context.Connection;

            var sources = await connection.QueryAsync<Source>(SqlConstants.GetSourcesByUsername, new {username});

            return sources.ToList();
        }

        public async Task Create(Source source)
        {
            using var connection = context.Connection;

            await connection.ExecuteAsync(SqlConstants.CreateSource, source);
        }
    }
}
