using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;

namespace CapstoneASP.Database.Repository
{
    public interface ITempContentRepository
    {
        public Task<Pdf> GetPdfById(int id);

        public Task<Video> GetVideoById(int id);
    }

    public class TempContentRepository : ITempContentRepository
    {
        private readonly DBContext.DBContext context;

        public TempContentRepository(DBContext.DBContext context)
        {
            this.context = context;
        }
        public async Task<Pdf> GetPdfById(int id)
        {
            using var connection = this.context.Connection;
            var pdf = await connection.QuerySingleOrDefaultAsync<Pdf>(SqlConstants.GetPdf, new { id });

            return pdf!;
        }

        public async Task<Video> GetVideoById(int id)
        {
            using var connection = this.context.Connection;
            var video = await connection.QuerySingleOrDefaultAsync<Video>(SqlConstants.GetVideo, new { id });

            return video!;
        }
    }
}
