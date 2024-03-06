namespace DesktopCapstone.DAL;

/// <summary>
///     The database connection string
/// </summary>
public class Connection
{
    #region Data members

    public static string ConnectionString =
        "Host=localhost;Port=5432;Database=postgres;Username= postgres;Password= root; Include Error Detail=True";

    #endregion
}