using CapstoneASP.Database.Repository;
using System.Threading.Tasks;
using CapstoneASP.Model;

namespace CapstoneASP.Database.Service
{
    /// <summary>
    /// Interface for tag-related operations.
    /// </summary>
    public interface ITagService
    {
        #region Methods

        /// <summary>
        /// Deletes a tag by its ID asynchronously.
        /// </summary>
        /// <param name="tagId">The ID of the tag to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteTagById(int tagId);

        Task<IEnumerable<Tags>> GetTagsBelongingToUser(string username);

        #endregion
    }

    /// <summary>
    /// Service class for managing tag-related operations.
    /// </summary>
    public class TagService : ITagService
    {
        #region Data Members

        private readonly ITagRepository tagRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TagService"/> class.
        /// </summary>
        /// <param name="tagRepository">The tag repository.</param>
        public TagService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public async Task DeleteTagById(int tagId)
        {
            await this.tagRepository.DeleteTagById(tagId);
        }

        public async Task<IEnumerable<Tags>> GetTagsBelongingToUser(string username)
        {
            return await this.tagRepository.GetTagsBelongingToUser(username);
        }

        #endregion
    }
}

