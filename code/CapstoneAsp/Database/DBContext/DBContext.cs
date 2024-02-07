using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Diagnostics.CodeAnalysis;

namespace CapstoneASP.Database.DBContext;

[ExcludeFromCodeCoverage]
public class DBContext : DbContext
{
    #region Properties

    public NpgsqlConnection Connection => new(Database.GetDbConnection().ConnectionString);

    #endregion

    #region Constructors

    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    #endregion
}