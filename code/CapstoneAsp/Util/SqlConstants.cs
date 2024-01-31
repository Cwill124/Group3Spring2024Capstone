namespace CapstoneASP.Util
{
    public class SqlConstants
    {
        #region UserLogin


        public const string GetUserLogin = "select * from capstone.login where username=@Username";

        public const string CreateUserLogin = "insert into capstone.login(username,password) values (@Username, @Password)";

        #endregion

        #region User

        public const string CreateUser = "INSERT INTO capstone.app_user(username) values (@Username);";

        public const string GetUserByUsername = "select * from capstone.app_user where username=@Username";

        #endregion



    }
}
