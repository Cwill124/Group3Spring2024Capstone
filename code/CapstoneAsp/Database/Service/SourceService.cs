using CapstoneASP.Database.Repository;
using CapstoneASP.Model;

namespace CapstoneASP.Database.Service;

public interface ISourceService
{
    #region Methods

    public Task<IEnumerable<Source>> GetSourceByUsername(string username);

    public Task Create(Source source);

    public Task<Source> GetByName(string name);

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

    public async Task<Source> GetByName(string name)
    {
        var source = await this.repository.GetByName(name);

        return source;
    }

    #endregion
}