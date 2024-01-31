using CapstoneASP.Database.Repository;
using CapstoneASP.Model;

namespace CapstoneASP.Database.Service
{
    public interface IUserService
    {
        public Task<User> GetUserByUsername(User user);
    }
    public class UserService : IUserService
    {
        #region properties

        private IUserRepository repository;

        #endregion

        #region Constructors

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Methods

        public async Task<User> GetUserByUsername(User user)
        {
            var result = await this.repository.GetUserByUsername(user);

            return result;
        }

        #endregion
    }
}
