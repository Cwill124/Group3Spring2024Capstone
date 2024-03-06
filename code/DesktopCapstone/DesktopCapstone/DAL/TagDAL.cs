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

        public void DeleteTag(Tags tag)
        {
            this.dbConnection.Open();
            this.dbConnection.Execute(SqlConstants.DeleteTag, tag);
            this.dbConnection.Close();
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
