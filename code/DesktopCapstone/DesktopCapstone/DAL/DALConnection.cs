using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using desktop_capstone.DAL;

namespace DesktopCapstone.DAL
{
    /// <summary>
    /// A singleton that contains all references to the DALS to prevent redundant creation
    /// </summary>
    public class DALConnection
    {
        public static SourceDAL SourceDAL = new SourceDAL();

        public static NoteDAL NoteDAL = new NoteDAL();
    }

   

}
