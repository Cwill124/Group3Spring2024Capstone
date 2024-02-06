using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CapstoneASP.Database.DBContext;

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