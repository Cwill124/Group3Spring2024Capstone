using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCapstone.DAL
{
    public class TagDAL
    {
        private IDbConnection dbConnection;

        public TagDAL(IDbConnection connection)
        {
            this.dbConnection = connection;
        }

        
    }
}
