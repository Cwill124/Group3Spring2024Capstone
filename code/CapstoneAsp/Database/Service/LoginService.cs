using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using static CapstoneASP.Database.Service.LoginService;

namespace CapstoneASP.Database.Service
{
    public interface ILoginService
    {
        public Task<UserLogin> GetUserLogin(UserLogin user);
    }
    public class LoginService : ILoginService
    {
        private ILoginRepository repository;
        
        public LoginService(ILoginRepository loginRepository) 
        {
            this.repository = loginRepository;
        }

        public async Task<UserLogin> GetUserLogin(UserLogin user)
        {
           var foundUser = await this.repository.GetUserLogin(user);

           return foundUser;
        }
    }
}
