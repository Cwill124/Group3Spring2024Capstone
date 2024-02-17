using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneASP.Model;

/// <summary>
///     Represents a UserLogin entity with properties such as UserId, Username, and Password.
/// </summary>
public class UserLogin
{
    #region Properties

    /// <summary>
    ///     Gets or sets the unique identifier for the user login.
    /// </summary>
    [Column("login_id")]
    public int? UserId { get; set; }

    /// <summary>
    ///     Gets or sets the username of the user.
    /// </summary>
    [Column("username")]
    public string Username { get; set; }

    /// <summary>
    ///     Gets or sets the password of the user.
    /// </summary>
    [Column("password")]
    public string Password { get; set; }

    #endregion

    #region Methods

    /// <summary>
    ///     Determines whether the current object is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>True if the objects are equal; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var otherUserLogin = (UserLogin)obj;
        return this.UserId == otherUserLogin.UserId &&
               string.Equals(this.Username, otherUserLogin.Username, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Serves as a hash function for the current object.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return this.UserId.GetHashCode();
    }

    #endregion
}