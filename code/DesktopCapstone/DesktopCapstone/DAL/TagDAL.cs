using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public int CreateTag(Tags tag) {
            this.dbConnection.Open();
            var result = this.dbConnection.Execute(SqlConstants.CreateTag,tag);
            this.dbConnection.Close();
            return result;
        }

        
        public int DeleteTag(Tags tag)
        {
            this.dbConnection.Open();
            var result = this.dbConnection.Execute(SqlConstants.DeleteTag, tag);
            this.dbConnection.Close();
            return result;
        }
        public ObservableCollection<Tags> GetTagsBelongingToUser(string username)
        {
            var tags = new ObservableCollection<Tags>();
            this.dbConnection.Open();
            var result = dbConnection.Query<Tags>(SqlConstants.GetTagsBelongingToUser, new { username });
            foreach (var item in result.ToList())
            {
                tags.Add(item);
            }
            this.dbConnection.Close();
            return tags;
        }
    }
}
