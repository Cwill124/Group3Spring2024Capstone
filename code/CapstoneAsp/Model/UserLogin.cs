using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneASP.Model;

public class UserLogin
{
    #region Properties

    [Column("username")] public string Username { get; set; }

    [Column("password")] public string Password { get; set; }
    
    #endregion
}