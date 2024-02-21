using System.Diagnostics.CodeAnalysis;
using CapstoneASP.Database.Service;
using CapstoneASP.Model;
using Microsoft.AspNetCore.Mvc;

/// <summary>
///     Controller for managing user-related operations.
/// </summary>

public class UserController : ControllerBase
{
    #region Data members

    private readonly IConfiguration _config;
    private readonly IUserService userService;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserController" /> class.
    /// </summary>
    /// <param name="config">The configuration.</param>
    /// <param name="service">The user service.</param>
    public UserController(IConfiguration config, IUserService service)
    {
        this._config = config;
        this.userService = service;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Retrieves a user by their username.
    /// </summary>
    /// <param name="user">The user object containing the username.</param>
    /// <returns>An IActionResult representing the operation result.</returns>
    [HttpPost]
    [Route("/GetUserByUsername")]
    public async Task<IActionResult> GetUserByUsername([FromBody] User user)
    {
        try
        {
            var result = await this.userService.GetUserByUsername(user);


            return Ok(result); // Successful operation
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving user: {ex.Message}"); // Handle exceptions
        }
    }

    #endregion
}