using CapstoneASP.Database.Repository;

namespace CapstoneASP.Database.Service
{
    public interface ITagService {
        #region Methods

        public Task DeleteTagById(int tagId);

        #endregion
    }
    public class TagService : ITagService
    {

        #region Data members

        private readonly ITagRepository tagRepository;


        #endregion

        #region Constructors


        public TagService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        #endregion

        #region Methods

         public async Task DeleteTagById(int tagId)
         {
             await this.tagRepository.DeleteTagById(tagId);
         }

        #endregion
        
    }
}
