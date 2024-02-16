using CapstoneASP.Database.Repository;
using CapstoneASP.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneASP.Database.Service
{
    /// <summary>
    /// Represents a service interface for source-related operations.
    /// </summary>
    public interface ISourceService
    {
        #region Methods

        /// <summary>
        /// Retrieves sources associated with a specific username.
        /// </summary>
        /// <param name="username">The username for which sources are to be retrieved.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a collection of sources.</returns>
        Task<IEnumerable<Source>> GetSourceByUsername(string username);

        /// <summary>
        /// Creates a new source with the specified details.
        /// </summary>
        /// <param name="source">The source details to be created.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Create(Source source);

        /// <summary>
        /// Retrieves a source based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the source to be retrieved.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the retrieved Source object.</returns>
        Task<Source> GetById(int id);

        /// <summary>
        /// Deletes a source based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the source to be deleted.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Delete(int id);

        #endregion
    }

    /// <summary>
    /// Represents a service implementation for source-related operations.
    /// </summary>
    public class SourceService : ISourceService
    {
        #region Data members

        private readonly ISourceRepository repository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceService"/> class with the specified repository.
        /// </summary>
        /// <param name="repository">The repository for source-related operations.</param>
        public SourceService(ISourceRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public async Task<IEnumerable<Source>> GetSourceByUsername(string username)
        {
            var sources = await this.repository.GetSourcesByUsername(username);

            return sources.ToList();
        }

        /// <inheritdoc/>
        public async Task Create(Source source)
        {
            await this.repository.Create(source);
        }

        /// <inheritdoc/>
        public async Task<Source> GetById(int id)
        {
            var source = await this.repository.GetById(id);

            return source;
        }

        /// <inheritdoc/>
        public async Task Delete(int id)
        {
            await this.repository.Delete(id);
        }

        #endregion
    }
}
