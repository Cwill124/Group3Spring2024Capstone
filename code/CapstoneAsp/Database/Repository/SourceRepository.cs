using CapstoneASP.Model;
using CapstoneASP.Util;
using Dapper;
using Newtonsoft.Json;

namespace CapstoneASP.Database.Repository;

public interface ISourceRepository
{
    #region Methods

    public Task<IEnumerable<Source>> GetSourcesByUsername(string username);
    public Task Create(Source source);

    public Task<Source> GetByName(string name);

    #endregion
}

public class SourceRepository : ISourceRepository
{
    #region Data members

    private readonly DBContext.DBContext context;

    #endregion

    #region Constructors

    public SourceRepository(DBContext.DBContext context)
    {
        this.context = context;
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<Source>> GetSourcesByUsername(string username)
    {
        using var connection = this.context.Connection;



        var dyResult = await connection.QueryAsync<dynamic>(SqlConstants.GetSourcesByUsername, new { username });
        var sources = new List<Source>();

        foreach (var item in dyResult)
        {
            var source = new Source()
            {
                SourceId = item.source_id,
                SourceTypeId = item.source_type_id,
                Content = item.content,
                MetaData = item.meta_data,
                Tags = item.tags,
                CreatedBy = item.created_by,
                Description = item.description,
                Name = item.name,

            };
            sources.Add(source);
        }

        return sources.ToList();
    }

    public async Task Create(Source source)
    {
        using var connection = this.context.Connection;

        await connection.ExecuteAsync(SqlConstants.CreateSource, source);
    }

    public Task<Source> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    #endregion
}