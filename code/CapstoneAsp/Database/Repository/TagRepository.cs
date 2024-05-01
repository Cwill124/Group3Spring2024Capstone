using CapstoneASP.Database.DBContext;
using CapstoneASP.Model;
using CapstoneASP.Util;

namespace CapstoneASP.Database.Repository
{
    /// <summary>
    /// Interface for operations related to Tags in the database.
    /// </summary>
    public interface ITagRepository
    {
        /// <summary>
        /// Creates a new tag in the database.
        /// </summary>
        /// <param name="tag">The tag object to be created.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task CreateTag(Tags tag);

        /// <summary>
        /// Retrieves tags associated with a particular note from the database.
        /// </summary>
        /// <param name="noteId">The ID of the note to retrieve tags for.</param>
        /// <returns>A collection of tags associated with the specified note.</returns>
        Task<IEnumerable<Tags>> GetTagsByNoteId(int noteId);
      
        /// <summary>
        /// Delete tag by it's Id from the database
        /// </summary>
        /// <param name="tagId">The id of the tag to delete</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task DeleteTagById(int tagId);

        Task<IEnumerable<Tags>> GetTagsBelongingToUser(string username);

        Task CheckForTagForNote(Tags tag);
    }


    /// <summary>
    /// Repository class for performing database operations related to Tags.
    /// </summary>
    public class TagRepository : ITagRepository
    {
        private readonly IDataContext context;

        /// <summary>
        /// Initializes a new instance of the TagRepository class with the specified data context.
        /// </summary>
        /// <param name="context">The data context to use for performing database operations.</param>
        public TagRepository(IDataContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task CreateTag(Tags tag)
        {
            using var connection = await this.context.CreateConnection();
            await connection.ExecuteAsync(SqlConstants.CreateTag, tag);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Tags>> GetTagsByNoteId(int noteId)
        {
            using var connection = await this.context.CreateConnection();
            var tags = await connection.QueryAsync<Tags>(SqlConstants.GetTagByNoteId, new { noteId });
            return tags;
        }
        ///<inheritdoc/>
        public async Task DeleteTagById(int tagId)
        {
            using var connection = await this.context.CreateConnection();

            await connection.ExecuteAsync(SqlConstants.DeleteTagById,new {tagId});
        }

        public async Task<IEnumerable<Tags>> GetTagsBelongingToUser(string username)
        {
            using var connection = await this.context.CreateConnection();
            var tags = await connection.QueryAsync<Tags>(SqlConstants.GetTagsBelongingToUser, new { username });
            return tags;
        }

        public async Task CheckForTagForNote(Tags tag)
        {
            using var connection = await this.context.CreateConnection();
            var result = await connection.QueryAsync<Tags>(SqlConstants.CheckTagsForExistingTagForNote, tag);
            if (result.Count() > 0)
            {
                throw new ArgumentException("Tag already exists for note");
            }
        }
    }
}

