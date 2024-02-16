using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneASP.Model;

/// <summary>
///     Represents a User entity with properties such as Username, Password, Email, Firstname, Lastname, and Phone.
/// </summary>
public class User
{
    #region Properties

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

    /// <summary>
    ///     Gets or sets the email address of the user.
    /// </summary>
    [Column("email")]
    public string? Email { get; set; }

    /// <summary>
    ///     Gets or sets the first name of the user.
    /// </summary>
    [Column("firstname")]
    public string? Firstname { get; set; }

    /// <summary>
    ///     Gets or sets the last name of the user.
    /// </summary>
    [Column("lastname")]
    public string? Lastname { get; set; }

    /// <summary>
    ///     Gets or sets the phone number of the user.
    /// </summary>
    [Column("phone")]
    public string? Phone { get; set; }

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

        var otherUser = (User)obj;
        return string.Equals(this.Username, otherUser.Username, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Serves as a hash function for the current object.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(this.Username);
    }

    #endregion
}