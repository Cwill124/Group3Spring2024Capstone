using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;
using CapstoneASP.Database.Repository;
using CapstoneASP.Model;

namespace CapstoneASP.Database.Service
{
    public interface ISourceService
    {
        public Task<IEnumerable<Source>> GetSourceByUsername(string username);

        public Task Create(Source source);


    }
    public class SourceService  : ISourceService
    {
        private readonly ISourceRepository repository;

        public SourceService(ISourceRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Source>> GetSourceByUsername(string username)
        {
            var sources = await this.repository.GetSourcesByUsername(username);

            return sources.ToList();
        }

        public async Task Create(Source source)
        {
            await this.repository.Create(source);
        }
    }
}
