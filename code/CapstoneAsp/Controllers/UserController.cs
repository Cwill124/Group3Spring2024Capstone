using System.Security.Claims;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneASP.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    #region Methods

    //For admin Only
    [HttpGet]
    [Route("Admins")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminEndPoint()
    {
        var currentUser = this.GetCurrentUser();
        return Ok($"Hi you are an {currentUser.Role}");
    }

    private User GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            var userClaims = identity.Claims;
            return new User
            {
                Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
            };
        }

        return null;
    }

    #endregion
}