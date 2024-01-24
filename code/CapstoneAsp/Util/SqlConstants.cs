namespace CapstoneASP.Util
{
    public class SqlConstants
    {
        #region UserLogin

        public const string GetUserLogin= "select * from app_user where username=@Username and password=@Password";

        #endregion

        public const string GetPdf = "select * from pdf where id=@id";

        public const string GetVideo = "select * from video_link where id=@id";
    }
}
