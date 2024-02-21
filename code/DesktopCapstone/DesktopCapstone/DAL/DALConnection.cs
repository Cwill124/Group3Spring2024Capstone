using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using desktop_capstone.DAL;
using Npgsql;

namespace DesktopCapstone.DAL
{
    /// <summary>
    /// A singleton that contains all references to the DALS to prevent redundant creation
    /// </summary>
    public class DALConnection
    {
        public static SourceDAL SourceDAL = new SourceDAL(new NpgsqlConnection(Connection.ConnectionString));

        public static NoteDAL NoteDAL = new NoteDAL(new NpgsqlConnection(Connection.ConnectionString));
    }

   

}
