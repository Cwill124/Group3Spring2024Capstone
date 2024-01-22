using CapstoneASP.Model;

namespace CapstoneASP.Database.Service
{
    public interface ILoginService
    {
            #region Methods
            Task<IEnumerable<Login>> GetUser();
        #endregion
    }

    public class LoginService
    {
        #region Data members


        #endregion
    }


}
