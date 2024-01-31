using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneASP.Controllers
{
    public class UserController : ControllerBase
    {
        #region Data members

        private readonly IConfiguration _config;

        private readonly IUserService userService;
        #endregion

        #region Constructors

        public UserController(IConfiguration config, IUserService service)
        {
            this._config = config;
            this.userService = service;
        }

        #endregion

        #region Methods

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("/GetUserByUsername")]
        public async Task<IActionResult> GetUserByUsername([FromBody] User user)
        {
            var result = await this.userService.GetUserByUsername(user);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        #endregion
    }
}
