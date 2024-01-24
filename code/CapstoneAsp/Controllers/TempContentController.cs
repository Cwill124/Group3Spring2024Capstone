using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneASP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TempContentController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly ITempContentService tempContentService;
        public TempContentController(IConfiguration config, ITempContentService service)
        {
            this._config = config;
            this.tempContentService = service;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("GetPDF")]
        public async Task<IActionResult> GetPdfById([FromBody] int id)
        {
            var pdf = await this.tempContentService.GetPdfById(id);

            return Ok(pdf);
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("GetVideo")]
        public async Task<IActionResult> GetVideoById([FromBody] int id)
        {
            var video = await this.tempContentService.GetVideoById(id);

            return Ok(video);
        }
    }
}
