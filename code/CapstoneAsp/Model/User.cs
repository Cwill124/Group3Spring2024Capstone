using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneASP.Model;

public class User
{
    #region Properties

    [Column("username")] public string Username { get; set; }

    [Column("email")] public string? Email { get; set; }

    [Column("firstname")] public string? Firstname { get; set; }

    [Column("lastname")] public string? Lastname { get; set; }

    [Column("phone")] public string? Phone { get; set; }

    #endregion

    #region Methods

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        User otherUser = (User)obj;
        return String.Equals(this.Username, otherUser.Username, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(Username);
    }

    #endregion
}