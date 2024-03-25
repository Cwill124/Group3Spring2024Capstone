using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

    public const string GetNotesByUsername = "select * from capstone.note where username = @username";
    public const string GetNoteLastAdded = "SELECT * FROM capstone.note ORDER BY capstone.note.note_id DESC LIMIT 1";

    public const string GetNoteWithTags = "SELECT * FROM capstone.note WHERE note_id = @noteId";

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

    public const string GetTagByNoteId = "SELECT * FROM capstone.tag where tag.note = @noteId";

    public const string DeleteTagById = "DELETE FROM capstone.tag WHERE tag_id = @tagId";

    public const string GetTagsBelongingToUser = "SELECT * FROM (SELECT t.*, ROW_NUMBER() OVER (PARTITION BY t.tag ORDER BY t.tag_id) AS row_num FROM capstone.tag t WHERE t.note IN (SELECT n.note_id FROM capstone.note n WHERE n.username = @username)) AS numbered_tags WHERE row_num = 1;";

    #endregion

    #region Project

    public const string CreateProject = "INSERT INTO capstone.project(title,description,owner) VALUES (@Title,@Description,@Owner);";

    public const string GetAllProjectsByOwner = "SELECT * FROM capstone.project WHERE owner = @owner;";

    public const string GetProjectById = "SELECT * FROM capstone.project WHERE project_id = @id;";

    public const string DeleteProject = "DELETE FROM capstone.project WHERE project_id = @id;";

    #endregion
}