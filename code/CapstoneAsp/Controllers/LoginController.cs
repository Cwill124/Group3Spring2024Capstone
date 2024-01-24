using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CapstoneASP.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    #region Data members

    private readonly IConfiguration _config;

    private readonly ILoginService loginService;
    #endregion

    #region Constructors

    public LoginController(IConfiguration config, ILoginService service)
    {
        this._config = config;
        this.loginService = service;
    }

    #endregion

    #region Methods

    [AllowAnonymous]
    [HttpPost]
    public ActionResult Login([FromBody] UserLogin userLogin)
    {

        var user = this.loginService.GetUserLogin(userLogin).Result;

        if (user != null)
        {
            var token = this.GenerateToken(user);
            return Ok(token);
        }

        return NotFound("user not found");
    }

    // To generate token
    private string GenerateToken(UserLogin user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var token = new JwtSecurityToken(this._config["Jwt:Issuer"], this._config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    //To authenticate user
    private User Authenticate(UserLogin userLogin)
    {
        var currentUser = UserConstants.Users.FirstOrDefault(x => x.Username.ToLower() ==
            userLogin.Username.ToLower() && x.Password == userLogin.Password);
        if (currentUser != null)
        {
            return currentUser;
        }

        return null;
    }

    #endregion
}