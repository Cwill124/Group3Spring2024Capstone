using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CapstoneASP.Database.DBContext;

/// <summary>
///     Represents the database context for the CapstoneASP application.
/// </summary>
[ExcludeFromCodeCoverage]
public class DBContext : DbContext
{
    #region Properties

    /// <summary>
    ///     Gets the NpgsqlConnection associated with the database context.
    /// </summary>
    public NpgsqlConnection Connection => new(Database.GetDbConnection().ConnectionString);

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="DBContext" /> class with the specified options.
    /// </summary>
    /// <param name="options">The options to be used for the context.</param>
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    #endregion
}