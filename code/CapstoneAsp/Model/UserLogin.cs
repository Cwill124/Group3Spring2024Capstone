using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneASP.Model;

public class UserLogin
{
    #region Properties

    [Column("user_id")] public int? UserId { get; set; }

    [Column("username")] public string Username { get; set; }

    [Column("password")] public string Password { get; set; }

    [Column("email")] public string? Email { get; set; }

    [Column("firstname")] public string? Firstname { get; set; }

    [Column("lastname")] public string? Lastname { get; set;}

    [Column("phone")] public string? Phone { get; set; }

    [Column("role")] public string? Role { get; set; }

    [Column("created_at")] public string? CreatedAt { get; set; }






    #endregion
}