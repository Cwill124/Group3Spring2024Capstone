using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CapstoneASP.Database.DBContext;


public interface IDataContext
{
    #region Methods

    Task<IDbConnectionWrapper> CreateConnection();

    void Init();

    #endregion
}

/// <summary>
///     Represents the database context for the CapstoneASP application.
/// </summary>
[ExcludeFromCodeCoverage]
public class DBContext : IDataContext
{
    #region Data members

    protected readonly IConfiguration Configuration;

    protected NpgsqlDataSource DataSource;

    #endregion

    public DBContext(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public async Task<IDbConnectionWrapper> CreateConnection()
    {
        var connection = await this.DataSource.OpenConnectionAsync();
        var connectionWrapper = new NpgsqlConnectionWrapper(connection);

        return connectionWrapper;
    }

    public void Init()
    {
        var connectionString =
            this.Configuration.GetConnectionString("DefaultConnection");

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        this.DataSource = dataSourceBuilder.Build();
    }
}