using CapstoneASP.Database.Repository;
using CapstoneASP.Model;

namespace CapstoneASP.Database.Service
{
    public interface ITempContentService
    {
        public Task<Pdf> GetPdfById(int id);

        public Task<Video> GetVideoById(int id);
    }
    public class TempContentService : ITempContentService
    {
        private readonly ITempContentRepository repository;
        public TempContentService(ITempContentRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Pdf> GetPdfById(int id)
        {
            return await this.repository.GetPdfById(id);

        }

        public async Task<Video> GetVideoById(int id)
        {
            return await this.repository.GetVideoById(id);
        }
    }
}
