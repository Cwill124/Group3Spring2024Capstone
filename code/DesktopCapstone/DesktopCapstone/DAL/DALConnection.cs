using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DesktopCapstone.DAL
{
    /// <summary>
    /// A singleton that contains all references to the DALS to prevent redundant creation
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DALConnection
    {
        public static SourceDAL SourceDAL = new SourceDAL(new NpgsqlConnection(Connection.ConnectionString));

        public static NoteDAL NoteDAL = new NoteDAL(new NpgsqlConnection(Connection.ConnectionString));

        public static TagDAL TagDal = new TagDAL(new NpgsqlConnection(Connection.ConnectionString));
    }

   

}
