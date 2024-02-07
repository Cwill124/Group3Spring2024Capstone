using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneASP.Model;

public class UserLogin
{
    #region Properties

    [Column("login_id")] public int? UserId { get; set; }
    [Column("username")] public string Username { get; set; }

    [Column("password")] public string Password { get; set; }

    #endregion

    #region Methods

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        UserLogin otherUserLogin = (UserLogin)obj;
        return this.UserId == otherUserLogin.UserId && String.Equals(this.Username, otherUserLogin.Username, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + UserId.GetHashCode();
            hash = hash * 23 + StringComparer.OrdinalIgnoreCase.GetHashCode(Username);
            return hash;
        }
    }

    #endregion
}