using System.Diagnostics.CodeAnalysis;
using Npgsql;

namespace DesktopCapstone.DAL;

/// <summary>
///     A singleton that contains all references to the DALS to prevent redundant creation
/// </summary>
[ExcludeFromCodeCoverage]
public class DALConnection
{
    #region Data members

    public static SourceDAL SourceDAL = new(new NpgsqlConnection(Connection.ConnectionString));

    public static NoteDAL NoteDAL = new(new NpgsqlConnection(Connection.ConnectionString));

    public static TagDAL TagDal = new(new NpgsqlConnection(Connection.ConnectionString));

    #endregion
}