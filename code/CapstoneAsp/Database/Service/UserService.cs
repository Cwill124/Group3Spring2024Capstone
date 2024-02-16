using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using System.Threading.Tasks;

namespace CapstoneASP.Database.Service
{
    /// <summary>
    /// Represents a service interface for user-related operations.
    /// </summary>
    public interface IUserService
    {
        #region Methods

        /// <summary>
        /// Retrieves user information based on the provided user object.
        /// </summary>
        /// <param name="user">The user object containing the username to search for.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the retrieved User object.</returns>
        Task<User> GetUserByUsername(User user);

        #endregion
    }

    /// <summary>
    /// Represents a service implementation for user-related operations.
    /// </summary>
    public class UserService : IUserService
    {
        #region Data members

        #region Properties

        private readonly IUserRepository repository;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class with the specified repository.
        /// </summary>
        /// <param name="repository">The repository for user-related operations.</param>
        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public async Task<User> GetUserByUsername(User user)
        {
            var result = await this.repository.GetUserByUsername(user);

            return result;
        }

        #endregion
    }
}