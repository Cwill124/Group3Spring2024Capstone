using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Mvc;

/// <summary>
///     Controller for managing operations related to sources.
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

    [HttpPost]
    [Route("Sources/GetNotInProject")]
    public async Task<IEnumerable<Source>> GetAllNotInProject([FromBody] int projectId)
    {
        var sources = await this.sourceService.GetAllNotInProject(projectId);
        return sources;
    }
    [HttpPost]
    [Route("Sources/GetAllInProject")]
    public async Task<IEnumerable<Source>> GetAllSourcesInProject([FromBody] int projectId)
    {
        var sources = await this.sourceService.GetAllInProject(projectId);
        return sources;
    }

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