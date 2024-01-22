using System.Diagnostics.CodeAnalysis;
using Npgsql;
using webapi.Database.Context;

namespace CapstoneASP.Database.Context
{
    public interface IDataContext
    {
        #region Methods

        Task<IDbConnectionWrapper> CreateConnection();

        void Init();

        #endregion
    }
    [ExcludeFromCodeCoverage]
    public class PostgreSqlDataContext : IDataContext
    {
        #region Data members

        protected readonly IConfiguration Configuration;

        protected NpgsqlDataSource DataSource;

        #endregion

        #region Constructors

        public PostgreSqlDataContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        #endregion

        #region Methods

        public async Task<IDbConnectionWrapper> CreateConnection()
        {
            var connection = await this.DataSource.OpenConnectionAsync();
            var connectionWrapper = new NpgsqlConnectionWrapper(connection);

            return connectionWrapper;
        }

        public void Init()
        {
            var connectionString =
                this.Configuration.GetConnectionString("CapstonePostgreSQlDatabase");

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            this.DataSource = dataSourceBuilder.Build();
        }

        #endregion
    }
}
