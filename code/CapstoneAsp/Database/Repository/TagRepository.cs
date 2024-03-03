using CapstoneASP.Database.DBContext;
using CapstoneASP.Model;
using CapstoneASP.Util;

namespace CapstoneASP.Database.Repository
{
    public interface ITagRepository
    {
        #region Methods

        Task CreateTag(Tags tag);

        Task<IEnumerable<Tags>> GetTagsByNoteId(int noteId);
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
        public async Task CreateTag(Tags tag)
        {
            using var connection = await this.context.CreateConnection();

            await connection.ExecuteAsync(SqlConstants.CreateTag, tag);

        }

        public async Task<IEnumerable<Tags>>GetTagsByNoteId(int noteId)
        {
            using var connection = await this.context.CreateConnection();
            var tags = await connection.QueryAsync<Tags>(SqlConstants.GetTagByNoteId, new { noteId });

            return tags;
        }

        #endregion

    }
}
