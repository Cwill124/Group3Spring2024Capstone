using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Text;

namespace DesktopCapstone.util
{
    public class SqlConstants
    {
        #region AppUser

        public const string CreateAppUser = "insert into capstone.app_user values (@Username, @FirstName, @LastName, @PhoneNumber, @Email)";

        public const string GetAppUserByUsername = "select * from capstone.app_user where username = @username";

        #endregion

        #region LoginInfo

        public const string CreateLoginInfo = "insert into capstone.login (username, password) values (@Username, @Password)";

        #endregion

        #region Note

        public const string GetNotesById = "select * from capstone.note where source_id = @Id";

        public const string CreateNewNote = "insert into capstone.note (source_id, content, username) values (@SourceId, @Content::json, @Username) RETURNING * ";

        public const string DeleteNoteById = "DELETE FROM capstone.note where note.note_id =@id ";

        public const string GetNotesByName = "select * from capstone.note where content->>'note_Title' = @name AND username = @username";

        public const string GetNotesByNameContains = "select * from capstone.note where content->>'note_Title' LIKE '%' || @name || '%' AND username = @username";

        public const string GetNotesByTag = "select * from capstone.note where content->>'tags' = @tag AND username = @username";

        public const string GetNotesByUsername = "select * from capstone.note where username = @username";

        public const string UpdateNoteContent =
            "UPDATE capstone.note SET content = Cast(@Content AS JSON) WHERE note_id = @NoteId ";

        #endregion

        #region Source

        public const string GetAllSources = "select * from capstone.source";

        public const string GetSourceById = "select * from capstone.source where source_id = @id";

        public const string GetSourceTypes = "select * from capstone.source_type";

        public const string CreateSource = "insert into capstone.source (description, name, content, meta_data, source_type_id, tags, created_by) values (@Description, @Name, @Content::json, @MetaData::json, @SourceType, @Tags::json, @CreatedBy)";

        public const string DeleteSourceById = "DELETE FROM capstone.source WHERE source.source_id=@id";

        public const string SearchSourceByName = "select * from capstone.source where name = @name";

        #endregion

        #region Tag

        public const string GetTagsBelongingToUser = "SELECT * FROM (SELECT t.*, ROW_NUMBER() OVER (PARTITION BY t.tag ORDER BY t.tag_id) AS row_num FROM capstone.tag t WHERE t.note IN (SELECT n.note_id FROM capstone.note n WHERE n.username = @username)) AS numbered_tags WHERE row_num = 1;";

        #endregion

        #region Login

        public const string CheckLogin = "select * from capstone.login where username = @username";

        public const string CheckIfUsernameInUse = "select * from capstone.login where username = @username";

        #endregion

        #region Tag

        public const string CreateTag = "INSERT INTO capstone.tag(tag,note) VALUES (@Tag,@Note)";

        public const string GetTagsByNoteId = "SELECT * FROM capstone.tag where tag.note = @noteId";

        public const string DeleteTag = "DELETE FROM capstone.tag WHERE tag.tag_id = @TagId";

        #endregion
    }
}
