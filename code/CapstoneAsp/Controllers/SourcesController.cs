﻿using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CapstoneASP.Controllers;

[Route("")]
[ApiController]
public class SourcesController : ControllerBase
{
    #region Data members

    private readonly IConfiguration _config;

    private readonly ISourceService sourceService;

    #endregion

    #region Constructors

    public SourcesController(IConfiguration config, ISourceService service)
    {
        this._config = config;
        this.sourceService = service;
    }

    #endregion

    #region Methods

    [HttpPost]
    [Route("Sources/Create")]
    public async Task<IActionResult> CreateSource([FromBody] Source source)
    {
        if (source == null)
        {
            return BadRequest(source);
        }

        try
        {
            await this.sourceService.Create(source);
            return Ok(source);
        }
        catch (Exception ex)
        {
            return BadRequest(source);
        }
    }

    [HttpPost]
    [Route("Sources/GetByUsername")]
    public async Task<IEnumerable<Source>> GetBySourceByUsername([FromBody] string username)
    {
        if (username.IsNullOrEmpty())
        {
            return (IEnumerable<Source>)BadRequest(null);
        }

        var sources = await this.sourceService.GetSourceByUsername(username);

        return sources;
    }

    [HttpPost]
    [Route("Sources/GetByName")]
    public async Task<Source> GetSourceByName([FromBody] string name)
    {
        if (name.IsNullOrEmpty())
        {
            return null!;
        }

        var source = await this.sourceService.GetByName(name);
        return source;
    }

    #endregion
}