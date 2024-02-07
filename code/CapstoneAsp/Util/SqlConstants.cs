using System.Diagnostics.CodeAnalysis;

namespace CapstoneASP.Util;

[ExcludeFromCodeCoverage]
public class SqlConstants
{
    #region Data members

    #region Note

    public const string CreateNote =
        "INSERT INTO capstone.note(source_id,content,username) VALUES(@SourceId,CAST(@Content AS JSON),@Username)";

    public const string GetNotesBySourceId = "SELECT note.note_id,note.source_id,note.content::text,note.username from capstone.note where note.source_id =@sourceId";

    public const string DeleteNote = "DELETE FROM capstone.note where note.note_id =@noteId ";
    #endregion

    #endregion

    #region UserLogin

    public const string GetUserLogin = "select * from capstone.login where username=@Username";

    public const string CreateUserLogin = "insert into capstone.login(username,password) values (@Username, @Password)";

    #endregion

    #region User

    public const string CreateUser = "INSERT INTO capstone.app_user(username) values (@Username);";

    public const string GetUserByUsername = "select * from capstone.app_user where username=@Username";

    #endregion

    #region Source

    public const string CreateSource =
        "INSERT INTO capstone.source (description, name, content, meta_data, tags, created_by, source_type_id) VALUES (@Description, @Name, CAST(@Content AS json), CAST(@MetaData AS json), CAST(@Tags AS json), @CreatedBy, @SourceTypeId);";

    public const string GetSourcesByUsername =
        "SELECT source.source_id, source.name, source.description, source.content::text, source.meta_data::text, source.tags::text, source.source_type_id,source.created_by FROM capstone.source WHERE source.created_by=@Username";

    public const string GetSourceById =
        "SELECT source.source_id, source.name, source.description, source.content::text, source.meta_data::text, source.tags::text, source.source_type_id,source.created_by FROM capstone.source where source.source_id=@id";

    public const string DeleteById = "DELETE FROM capstone.source WHERE  source.source_id=@id";

    #endregion
}