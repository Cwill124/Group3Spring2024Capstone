using CapstoneASP.Database.DBContext;
using CapstoneASP.Model;
using CapstoneASP.Util;

namespace CapstoneASP.Database.Repository
{
    public interface ITagRepository
    {
        #region Methods

        Task CreateTag(Tag tag);

        Task<IEnumerable<Tag>> GetTagsByNoteId(int noteId);
        #endregion
    }

    public class TagRepository : ITagRepository
    {
        #region Data members

        private readonly IDataContext context;

        #endregion

        #region Constructor

        public TagRepository(IDataContext context)
        {
            this.context = context;
        }
        #endregion

        #region Methods
        public async Task CreateTag(Tag tag)
        {
            using var connection = await this.context.CreateConnection();

            await connection.ExecuteAsync(SqlConstants.CreateTag, tag);

        }

        public async Task<IEnumerable<Tag>>GetTagsByNoteId(int noteId)
        {
            using var connection = await this.context.CreateConnection();
            var tags = await connection.QueryAsync<Tag>(SqlConstants.GetTagByNoteId, new { noteId });

            return tags;
        }

        #endregion

    }
}
