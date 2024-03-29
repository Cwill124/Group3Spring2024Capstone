using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Mvc;

/// <summary>
///     Controller that manages all source API requests
/// </summary>
[Route("")]
[ApiController]
public class SourcesController : ControllerBase
{
    #region Data members

    private readonly IConfiguration _config;
    private readonly ISourceService sourceService;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="SourcesController" /> class.
    /// </summary>
    /// <param name="config">The configuration.</param>
    /// <param name="service">The source service.</param>
    public SourcesController(IConfiguration config, ISourceService service)
    {
        this._config = config;
        this.sourceService = service;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Creates a new source.
    ///     if a source is created successfully returns OK with the message source created successfully 
    ///     otherwise BadRequest is returned with the exception message
    /// </summary>
    /// <param name="source">The source to create.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpPost]
    [Route("Sources/Create")]
    public async Task<IActionResult> CreateSource([FromBody] Source source)
    {
        try
        {
            await this.sourceService.Create(source);
            return Ok("Source created successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create source: {ex.Message}");
        }
    }

    /// <summary>
    ///     Retrieves sources based on the provided username.
    /// </summary>
    /// <param name="username">The username to filter sources.</param>
    /// <returns>A collection of sources associated with the given username.</returns>
    [HttpPost]
    [Route("Sources/GetByUsername")]
    public async Task<IEnumerable<Source>> GetBySourceByUsername([FromBody] string username)
    {
        var sources = await this.sourceService.GetSourceByUsername(username);

        return sources;
    }

    /// <summary>
    ///     Retrieves a source by its unique identifier.
    /// </summary>
    /// <param name="id">The identifier of the source to retrieve.</param>
    /// <returns>The source with the specified identifier.</returns>
    [HttpPost]
    [Route("Sources/GetById")]
    public async Task<Source> GetSourceById([FromBody] int id)
    {
        var source = await this.sourceService.GetById(id);
        return source;
    }

    /// <summary>
    ///     Deletes a source by its unique identifier.
    ///     If source is deleted successfully returns OK with the message below
    ///     otherwise returns a BadRequest with exception message
    /// </summary>
    /// <param name="id">The identifier of the source to delete.</param>
    /// <returns>An asynchronous task representing the deletion operation.</returns>
    [HttpPost]
    [Route("Sources/DeleteById")]
    public async Task<IActionResult> DeleteById([FromBody] int id)
    {
        try
        {
            await this.sourceService.Delete(id);
            return Ok("Source deleted successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to delete source: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves all sources that are not associated with a specific project.
    /// </summary>
    /// <param name="projectId">The identifier of the project.</param>
    /// <returns>A collection of sources not associated with the project.</returns>
    [HttpPost]
    [Route("Sources/GetNotInProject")]
    public async Task<IEnumerable<Source>> GetAllNotInProject([FromBody] int projectId)
    {
        var sources = await this.sourceService.GetAllNotInProject(projectId);
        return sources;
    }

    /// <summary>
    /// Retrieves all sources that are associated with a specific project.
    /// </summary>
    /// <param name="projectId">The identifier of the project.</param>
    /// <returns>A collection of sources associated with the project.</returns>
    [HttpPost]
    [Route("Sources/GetAllInProject")]
    public async Task<IEnumerable<Source>> GetAllSourcesInProject([FromBody] int projectId)
    {
        var sources = await this.sourceService.GetAllInProject(projectId);
        return sources;
    }

    /// <summary>
    /// Adds multiple sources to a project.
    /// if the sources are added successfully returns OK otherwise return BadRequest with the exception message
    /// </summary>
    /// <param name="projectAndSources">The project and the sources to be added.</param>
    /// <returns>An IActionResult indicating the success of the operation.</returns>
    [HttpPost]
    [Route("Sources/AddSourceToProject")]
    public async Task<IActionResult> AddSourcesToProject([FromBody] ProjectAndSources projectAndSources)
    {
        try
        {
            await this.sourceService.AddSourcesToProject(projectAndSources);
            return Ok("Sources added successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to add source: {ex.Message}");
        }
    }

    /// <summary>
    /// Deletes A single source from the project with the potential in the future to delete multiple.
    /// if source is deleted successfully return OK otherwise return BadRequest with the failed to delete source
    /// </summary>
    /// <param name="projectAndSources">The project and the sources to be deleted.</param>
    /// <returns>An IActionResult indicating the success of the operation.</returns>
    [HttpDelete]
    [Route("Sources/DeleteSourceFromProject")]
    public async Task<IActionResult> DeleteSourceFromProject([FromBody] ProjectAndSources projectAndSources)
    {
        try
        {
            await this.sourceService.DeleteSourceFromProject(projectAndSources);
            return Ok("Sources Deleted successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to Delete source: {ex.Message}");
        }
    }

    #endregion
}