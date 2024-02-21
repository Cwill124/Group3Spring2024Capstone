using System.Diagnostics.CodeAnalysis;
using Npgsql;

namespace CapstoneASP.Database.DBContext
{
    /// <summary>
    /// Interface representing the data context for the CapstoneASP application.
    /// </summary>
    public interface IDataContext
    {
        #region Methods

        /// <summary>
        /// Creates a new database connection.
        /// </summary>
        /// <returns>Returns a task with an IDbConnectionWrapper representing the database connection.</returns>
        Task<IDbConnectionWrapper> CreateConnection();

        /// <summary>
        /// Initializes the data context.
        /// </summary>
        void Init();

        #endregion
    }

    /// <summary>
    /// Represents the database context for the CapstoneASP application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DBContext : IDataContext
    {
        #region Data members

        protected readonly IConfiguration Configuration;

        protected NpgsqlDataSource DataSource;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DBContext"/> class.
        /// </summary>
        /// <param name="configuration">The configuration instance.</param>
        public DBContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new database connection asynchronously.
        /// </summary>
        /// <returns>Returns a task with an IDbConnectionWrapper representing the database connection.</returns>
        public async Task<IDbConnectionWrapper> CreateConnection()
        {
            var connection = await this.DataSource.OpenConnectionAsync();
            var connectionWrapper = new NpgsqlConnectionWrapper(connection);

            return connectionWrapper;
        }

        /// <summary>
        /// Initializes the data context by setting up the database connection.
        /// </summary>
        public void Init()
        {
            var connectionString = this.Configuration.GetConnectionString("DefaultConnection");

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            this.DataSource = dataSourceBuilder.Build();
        }

        #endregion
    }
}
