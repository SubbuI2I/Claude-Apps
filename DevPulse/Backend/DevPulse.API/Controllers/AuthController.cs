using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DevPulse.Core.Services;
using System.Security.Claims;

namespace DevPulse.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Email and password are required");

        var result = await _authService.RegisterAsync(request.Email, request.Password, request.FullName);
        if (!result.Success)
            return BadRequest(new { message = result.Message });

        return Ok(new { token = result.Token, message = result.Message });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Email and password are required");

        var result = await _authService.LoginAsync(request.Email, request.Password);
        if (!result.Success)
            return Unauthorized(new { message = result.Message });

        return Ok(new { token = result.Token, message = result.Message });
    }

    [HttpGet("validate")]
    [Authorize]
    public async Task<IActionResult> Validate()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);
        return Ok(new { userId, email, isValid = true });
    }
}

public class RegisterRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? FullName { get; set; }
}

public class LoginRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
