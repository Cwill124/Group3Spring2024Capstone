using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using CapstoneASP.Util;
using Microsoft.AspNetCore.Identity;
using static CapstoneASP.Database.Service.LoginService;

namespace CapstoneASP.Database.Service
{
    public interface ILoginService
    {
        public Task<UserLogin> GetUserLogin(UserLogin user);
        public Task CreateAccount(UserLogin user);
    }
    public class LoginService : ILoginService
    {
        private ILoginRepository repository;

        private IUserRepository userRepository;
        
        public LoginService(ILoginRepository loginRepository, IUserRepository userRepository) 
        {
            this.repository = loginRepository;
            this.userRepository = userRepository;
        }

        public async Task<UserLogin> GetUserLogin(UserLogin user)
        {
            var foundUser = await this.repository.GetUserLogin(user);

            if (PasswordHasher.CheckHashedPassword(foundUser.Password, user.Password) == PasswordVerificationResult.Success)
            {
                return foundUser;
            }

            return null;
        }

        public async Task CreateAccount(UserLogin user)
        {
            user.Password = PasswordHasher.HashPassword(user.Password);
            await this.repository.CreateAccount(user);
            var newUser = new User
            {
                Username = user.Username
            };
            await this.userRepository.CreateUser(newUser);

        }
    }
}
