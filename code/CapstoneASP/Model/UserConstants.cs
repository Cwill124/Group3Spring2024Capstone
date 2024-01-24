namespace CapstoneASP.Model;

public class UserConstants
{
    #region Data members

    public static List<User> Users = new()
    {
        new User { Username = "name", Password = "pass", Role = "Admin" }
    };

    #endregion
}