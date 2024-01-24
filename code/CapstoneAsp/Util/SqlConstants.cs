namespace CapstoneASP.Util
{
    public class SqlConstants
    {
        #region UserLogin

        public const string GetUserLogin= "select * from app_user where username=@Username and password=@Password";

        #endregion
    }
}
