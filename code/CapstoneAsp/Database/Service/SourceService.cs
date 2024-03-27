using CapstoneASP.Database.Repository;
using CapstoneASP.Model;

namespace CapstoneASP.Database.Service;

/// <summary>
///     Represents a service interface for source-related operations.
/// </summary>
public interface ISourceService
{
    #region Methods

    /// <summary>
    ///     Retrieves sources associated with a specific username.
    /// </summary>
    /// <param name="username">The username for which sources are to be retrieved.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation, containing a collection of sources.</returns>
    Task<IEnumerable<Source>> GetSourceByUsername(string username);

    /// <summary>
    ///     Creates a new source with the specified details.
    /// </summary>
    /// <param name="source">The source details to be created.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task Create(Source source);

    /// <summary>
    ///     Retrieves a source based on its identifier.
    /// </summary>
    /// <param name="id">The identifier of the source to be retrieved.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation, containing the retrieved Source object.</returns>
    Task<Source> GetById(int id);

    /// <summary>
    ///     Deletes a source based on its identifier.
    /// </summary>
    /// <param name="id">The identifier of the source to be deleted.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task Delete(int id);
    /// <summary>
    /// Retrieves all sources that are not associated with a specific project from the repository.
    /// </summary>
    /// <param name="projectId">The identifier of the project.</param>
    /// <returns>A task that represents the asynchronous operation and returns a collection of sources not associated with the project.</returns>
    Task<IEnumerable<Source>> GetAllNotInProject(int projectid);
    /// <summary>
    /// Retrieves all sources that are associated with a specific project from the repository.
    /// </summary>
    /// <param name="projectId">The identifier of the project.</param>
    /// <returns>A task that represents the asynchronous operation and returns a collection of sources associated with the project.</returns>
    Task<IEnumerable<Source>> GetAllInProject(int projectid);
    /// <summary>
    /// Adds multiple sources to a project in the repository.
    /// </summary>
    /// <param name="projectAndSources">The project and the sources to be added.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddSourcesToProject(ProjectAndSources  projectAndSources);
    /// <summary>
    /// Deletes multiple sources from a project in the repository.
    /// </summary>
    /// <param name="projectAndSources">The project and the sources to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteSourceFromProject(ProjectAndSources projectAndSources);

    #endregion
}

/// <summary>
///     Represents a service implementation for source-related operations.
/// </summary>
public class SourceService : ISourceService
{
    #region Data members

    private readonly ISourceRepository repository;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="SourceService" /> class with the specified repository.
    /// </summary>
    /// <param name="repository">The repository for source-related operations.</param>
    public SourceService(ISourceRepository repository)
    {
        this.repository = repository;
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public async Task<IEnumerable<Source>> GetSourceByUsername(string username)
    {
        var sources = await this.repository.GetSourcesByUsername(username);

        return sources.ToList();
    }

    /// <inheritdoc />
    public async Task Create(Source source)
    {
        await this.repository.Create(source);
    }

    /// <inheritdoc />
    public async Task<Source> GetById(int id)
    {
        var source = await this.repository.GetById(id);

        return source;
    }

    /// <inheritdoc />
    public async Task Delete(int id)
    {
        await this.repository.Delete(id);
    }
    /// <inheritdoc />
    public async Task<IEnumerable<Source>> GetAllNotInProject(int projectid)
    {
        return await this.repository.GetAllNotInProject(projectid);
    }
    /// <inheritdoc />
    public async Task<IEnumerable<Source>> GetAllInProject(int projectid)
    {
        return await this.repository.GetAllInProject(projectid);
    }
    /// <inheritdoc />
    public async Task AddSourcesToProject(ProjectAndSources projectAndSources)
    {
        foreach (var source in projectAndSources.sources)
        {
            await this.repository.AddSourceToProject(source, projectAndSources.projectId);
        }
    }
    /// <inheritdoc />
    public async Task DeleteSourceFromProject(ProjectAndSources projectAndSources)
    {
        foreach (var source in projectAndSources.sources)
        {
            await this.repository.DeleteSourceFromProject(source, projectAndSources.projectId);
        }
    }

    #endregion
}