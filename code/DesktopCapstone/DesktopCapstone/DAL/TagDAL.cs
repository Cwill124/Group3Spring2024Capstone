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
        private IDbConnection dbConnection;

        public TagDAL(IDbConnection connection)
        {
            this.dbConnection = connection;
        }

        public ObservableCollection<Tag> GetTagsBelongingToUser(string username)
        {
            var tags = new ObservableCollection<Tag>();
            this.dbConnection.Open();
            var result = dbConnection.Query<dynamic>(SqlConstants.GetTagsBelongingToUser, new { username });
            foreach (var item in result.ToList())
            {
                var newTag = new Tag()
                {
                    TagId = item.tag_id,
                    TagName = item.tag,
                    Note = item.note
                };
                tags.Add(newTag);
            }
            this.dbConnection.Close();
            return tags;
        }
    }
}
