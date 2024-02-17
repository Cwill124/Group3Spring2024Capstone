using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace desktop_capstone.DAL
{
    /// <summary>
    /// The database connection string 
    /// </summary>
    public class Connection
    {

        public static string ConnectionString = "Host=localhost;Port=5432;Database=postgres;Username= postgres;Password= root; Include Error Detail=True";

    }
}
