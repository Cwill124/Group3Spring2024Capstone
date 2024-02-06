using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CapstoneASP.Controllers
{
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
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("Sources/Create")]
        public async Task<IActionResult> CreateSource([FromBody] Source source)
        {
            if (source == null)
            {
                return BadRequest(source);
            }
            //try
            //{ 
                await this.sourceService.Create(source);
                return Ok(source);
            //}
            //catch (Exception ex)
            //{
                return BadRequest(source);
            //}
            
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("Sources/GetByUsername")]
        public async Task<IEnumerable<Source>> GetBySourceByUsername([FromBody] string username)
        {
            //if (username.IsNullOrEmpty())
            //{
            //    return (IEnumerable<Source>)BadRequest(null);
            //}

            var sources = await this.sourceService.GetSourceByUsername(username);

            return sources;
        }
    }
}
