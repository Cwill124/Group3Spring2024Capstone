﻿using CapstoneASP.Database.DBContext;
using CapstoneASP.Model;
using CapstoneASP.Util;

namespace CapstoneASP.Database.Repository;

/// <summary>
///     Represents a repository interface for source-related operations.
/// </summary>
public interface ISourceRepository
{
    #region Methods

    /// <summary>
    ///     Retrieves sources associated with a specific username.
    /// </summary>
    /// <param name="username">The username for which to retrieve sources.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation, containing a collection of sources.</returns>
    Task<IEnumerable<Source>> GetSourcesByUsername(string username);

    /// <summary>
    ///     Creates a new source with the specified details.
    /// </summary>
    /// <param name="source">The source details to be created.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task Create(Source source);

    /// <summary>
    ///     Retrieves a source based on its identifier.
    /// </summary>
    /// <param name="id">The identifier of the source to retrieve.</param>
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
    Task<IEnumerable<Source>> GetAllNotInProject(Project projectid);
    /// <summary>
    /// Retrieves all sources that are associated with a specific project from the repository.
    /// </summary>
    /// <param name="projectId">The identifier of the project.</param>
    /// <returns>A task that represents the asynchronous operation and returns a collection of sources associated with the project.</returns>
    Task<IEnumerable<Source>> GetAllInProject(int projectId);
    /// <summary>
    /// Adds a source to a project in the repository.
    /// </summary>
    /// <param name="sourceId">The identifier of the source.</param>
    /// <param name="projectId">The identifier of the project.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddSourceToProject(int sourceId, int projectId);
    /// <summary>
    /// Deletes a source from a project in the repository.
    /// </summary>
    /// <param name="sourceId">The identifier of the source.</param>
    /// <param name="projectId">The identifier of the project.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteSourceFromProject(int sourceId, int projectId);

    #endregion
}

/// <summary>
///     Represents a repository implementation for source-related operations.
/// </summary>
public class SourceRepository : ISourceRepository
{
    #region Data members

    private readonly IDataContext context;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="SourceRepository" /> class with the specified database context.
    /// </summary>
    /// <param name="context">The database context used for repository operations.</param>
    public SourceRepository(IDataContext context)
    {
        this.context = context;
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public async Task<IEnumerable<Source>> GetSourcesByUsername(string username)
    {
        using var connection = await this.context.CreateConnection();

        var sources = await connection.QueryAsync<Source>(SqlConstants.GetSourcesByUsername, new { username });

        return sources;
    }

    /// <inheritdoc />
    public async Task Create(Source source)
    {
        using var connection = await this.context.CreateConnection();

        await connection.ExecuteAsync(SqlConstants.CreateSource, source);
    }

    /// <inheritdoc />
    public async Task<Source> GetById(int id)
    {
        using var connection = await this.context.CreateConnection();
        var dyQuery = await connection.QueryAsync<Source>(SqlConstants.GetSourceById, new { id });
        var source = dyQuery.ElementAt(0);

        return source;
    }

    /// <inheritdoc />
    public async Task Delete(int id)
    {
        using var connection = await this.context.CreateConnection();

        await connection.ExecuteAsync(SqlConstants.DeleteById, new { id });
    }
    /// <inheritdoc />
    public async Task<IEnumerable<Source>> GetAllNotInProject(Project project)
    {
        using var connection = await this.context.CreateConnection();

        var sources = await connection.QueryAsync<Source>(SqlConstants.GetSourcesNotInProject, project);

        return sources;
    }
    /// <inheritdoc />
    public async Task<IEnumerable<Source>> GetAllInProject(int projectId)
    {
        using var connection = await this.context.CreateConnection();

        var sources = await connection.QueryAsync<Source>(SqlConstants.GetSourcesInProject, new { projectId });

        return sources;
    }
    /// <inheritdoc />
    public async Task AddSourceToProject(int sourceId, int projectId)
    {
        using var connection = await this.context.CreateConnection();

        await connection.ExecuteAsync(SqlConstants.AddSourceToProject, new { sourceId, projectId });
    }
    /// <inheritdoc />
    public async Task DeleteSourceFromProject(int sourceId, int projectId)
    {
        using var connection = await this.context.CreateConnection();

        await connection.ExecuteAsync(SqlConstants.DeleteSourceFromProject, new { sourceId, projectId });
    }

    #endregion
}