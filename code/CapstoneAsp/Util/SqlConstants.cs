using System.Diagnostics.CodeAnalysis;

namespace CapstoneASP.Util;

/// <summary>
///     Contains all Sql statements for the DB
/// </summary>
[ExcludeFromCodeCoverage]
public class SqlConstants
{
    #region Note

    public const string CreateNote =
        "INSERT INTO capstone.note(source_id,content,username) VALUES(@Source_Id,CAST(@Content AS JSON),@Username)";

    public const string GetNotesBySourceId =
        "SELECT * from capstone.note where note.source_id =@sourceId";

    public const string DeleteNote = "DELETE FROM capstone.note where note.note_id =@noteId ";

    #endregion

    #region UserLogin

    public const string GetUserLogin = "SELECT * from capstone.login where username=@Username";

    public const string CreateUserLogin = "INSERT into capstone.login(username,password) values (@Username, @Password)";

    #endregion

    #region User

    public const string CreateUser =
        "INSERT INTO capstone.app_user(username,fname,lname,phone,email) values (@Username,@Firstname,@Lastname,@Phone,@Email);";

    public const string GetUserByUsername = "select * from capstone.app_user where username=@Username";

    #endregion

    #region Source

    public const string CreateSource =
        "INSERT INTO capstone.source (description, name, content, meta_data, tags, created_by, source_type_id) VALUES (@Description, @Name, CAST(@Content AS json), CAST(@Meta_Data AS json), CAST(@Tags AS json), @Created_By, @Source_Type_Id);";

    public const string GetSourcesByUsername =
        "SELECT * FROM capstone.source WHERE source.created_by=@Username";

    public const string GetSourceById =
        "SELECT* FROM capstone.source where source.source_id=@id";

    public const string DeleteById = "DELETE FROM capstone.source WHERE  source.source_id=@id";

    #endregion
    #region Tag

    public const string CreateTag = "INSERT INTO capstone.tag(tag,note) VALUES (@Tag,@Note)";

    public const string GetTagByNoteId ="SELECT DISTINCT ON (capstone.tag) FROM capstone.tag WHERE note = @noteId";

    #endregion
}