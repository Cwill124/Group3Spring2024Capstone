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

        public const string CreateNewNote = "insert into capstone.note (source_id, content, username) values (@SourceId, @Content::json, @Username)";

        public const string DeleteNoteById = "DELETE FROM capstone.note where note.note_id =@id ";

        public const string GetNotesByName = "select * from capstone.note where content->>'noteTitle' = @name AND username = @username";

        public const string GetNotesByNameContains = "select * from capstone.note where content->>'noteTitle' LIKE '%' || @name || '%' AND username = @username";

        public const string GetNotesByTag = "select * from capstone.note where content->>'tags' = @tag AND username = @username";

        public const string GetNotesByUsername = "select * from capstone.note where username = @username";

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

        public const string CreateTag = "insert into capstone.tag (name, note_reference) values (@Name, @NoteReference)";

        public const string GetTagsByNoteId = "select * from capstone.tag where note = @id";

        public const string GetTagsByName = "select * from capstone.tag where name = @name";

        public const string DeleteTagById = "DELETE FROM capstone.tag WHERE tag.tag_id=@id";

        #endregion

        #region Login

        public const string CheckLogin = "select * from capstone.login where username = @username";

        public const string CheckIfUsernameInUse = "select * from capstone.login where username = @username";

        #endregion
    }
}
