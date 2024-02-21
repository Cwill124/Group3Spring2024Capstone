using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CapstoneASP.Controllers;

/// <summary>
///     Represents a controller for handling user login and registration operations.
/// </summary>

[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    #region Data members

    private readonly IConfiguration _config;
    private readonly ILoginService loginService;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="LoginController" /> class with the specified configuration and login
    ///     service.
    /// </summary>
    /// <param name="config">The configuration for JWT token generation.</param>
    /// <param name="service">The login service for handling user authentication and registration.</param>
    public LoginController(IConfiguration config, ILoginService service)
    {
        this._config = config;
        this.loginService = service;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Handles user login by verifying credentials and generating a JWT token upon successful authentication.
    /// </summary>
    /// <param name="userLogin">The user credentials for login.</param>
    /// <returns>An asynchronous task representing the HTTP response containing the generated JWT token or an error message.</returns>
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
        else
        {
            return NotFound("User not found");
        }

     
    }

    /// <summary>
    ///     Handles user registration by creating a new user account.
    /// </summary>
    /// <param name="userLogin">The user details for registration.</param>
    /// <returns>An asynchronous task representing the HTTP response indicating success or failure of the registration process.</returns>
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
            return BadRequest("Registration failed");
        }
    }

    /// <summary>
    ///     Generates a JWT token for the authenticated user.
    /// </summary>
    /// <param name="user">The authenticated user for whom the token is generated.</param>
    /// <returns>The generated JWT token as a string.</returns>
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