using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DesktopCapstone.model;
using DesktopCapstone.util;

namespace DesktopCapstone.DAL
{
    public class TagDAL
    {
        private readonly IDbConnection dbConnection;

        public TagDAL(IDbConnection connection)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            this.dbConnection = connection;
        }
        public void CreateTag(Tags tag) {
            this.dbConnection.Open();
            this.dbConnection.Execute(SqlConstants.CreateTag,tag);
            this.dbConnection.Close();
        }

        public IEnumerable<Tags> GetTagsByNoteId(int noteId)
        {
            this.dbConnection.Open();
            var tags = this.dbConnection.Query<Tags>(SqlConstants.GetTagsByNoteId, new { noteId });
            this.dbConnection.Close();
            return tags;
        }
    }
}
