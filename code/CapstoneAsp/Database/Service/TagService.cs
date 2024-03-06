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

        /// <summary>
        /// Retrieves a collection of tags belonging to a specific user.
        /// </summary>
        /// <param name="username">The username for which to retrieve tags.</param>
        /// <returns>An asynchronous task that represents the operation, returning a collection of Tags.</returns>
        Task<IEnumerable<Tags>> GetTagsBelongingToUser(string username);

        /// <summary>
        /// Creates a new tag using the provided Tag object.
        /// </summary>
        /// <param name="tag">The Tag object containing information about the new tag.</param>
        /// <returns>An asynchronous task that represents the operation.</returns>
        Task CreateTag(Tags tag);

        /// <summary>
        /// Retrieves a collection of tags associated with a specific note ID.
        /// </summary>
        /// <param name="noteId">The ID of the note for which to retrieve tags.</param>
        /// <returns>An asynchronous task that represents the operation, returning a collection of Tags.</returns>
        Task<IEnumerable<Tags>> GetTagsByNoteId(int noteId);
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
        /// <inheritdoc/>
        public async Task<IEnumerable<Tags>> GetTagsBelongingToUser(string username)
        {
            return await this.tagRepository.GetTagsBelongingToUser(username);
        }
        /// <inheritdoc/>
        public async Task CreateTag(Tags tag)
        {
            await this.tagRepository.CreateTag(tag);
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<Tags>> GetTagsByNoteId(int noteId)
        {
            var tags = await this.tagRepository.GetTagsByNoteId(noteId);

            return tags;
        }

        #endregion
    }
}

