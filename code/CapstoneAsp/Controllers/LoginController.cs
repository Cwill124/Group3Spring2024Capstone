using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CapstoneASP.Controllers;

[ExcludeFromCodeCoverage]
[Route("[controller]")]
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

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
    {
        var user = await this.loginService.GetUserLogin(userLogin);

        if (user != null)
        {
            var token = this.GenerateToken(user);

            return Ok(new { token });
        }

        return NotFound("user not found");
    }

    [HttpPost]
    [Route("/Register")]
    public async Task<IActionResult> Register([FromBody] User userLogin)
    {
        try
        {
            await this.loginService.CreateAccount(userLogin);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    // To generate token
    private string GenerateToken(UserLogin user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username)
        };
        var token = new JwtSecurityToken(this._config["Jwt:Issuer"], this._config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    #endregion
}