using CapstoneASP.Database.Repository;
using CapstoneASP.Model;

namespace CapstoneASP.Database.Service;

public interface ISourceService
{
    #region Methods

    public Task<IEnumerable<Source>> GetSourceByUsername(string username);

    public Task Create(Source source);

    public Task<Source> GetById(int id);

    public Task Delete(int id);

    #endregion
}

public class SourceService : ISourceService
{
    #region Data members

    private readonly ISourceRepository repository;

    #endregion

    #region Constructors

    public SourceService(ISourceRepository repository)
    {
        this.repository = repository;
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<Source>> GetSourceByUsername(string username)
    {
        var sources = await this.repository.GetSourcesByUsername(username);

        return sources.ToList();
    }

    public async Task Create(Source source)
    {
        await this.repository.Create(source);
    }

    public async Task<Source> GetById(int id)
    {
        var source = await this.repository.GetById(id);

        return source;
    }

    public async Task Delete(int id)
    {
        await this.repository.Delete(id);
    }

    #endregion
}