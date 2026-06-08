using DevPulse.Data;
using DevPulse.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using BCrypt.Net;

namespace DevPulse.Core.Services;

public interface IAuthService
{
    Task<(bool Success, string? Token, string? Message)> RegisterAsync(string email, string password, string? fullName);
    Task<(bool Success, string? Token, string? Message)> LoginAsync(string email, string password);
    Task<bool> ValidateTokenAsync(string token);
}

public class AuthService : IAuthService
{
    private readonly DevPulseContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(DevPulseContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<(bool Success, string? Token, string? Message)> RegisterAsync(string email, string password, string? fullName)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
        if (existingUser != null)
            return (false, null, "User already exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            FullName = fullName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = GenerateJwtToken(user);
        return (true, token, "Registration successful");
    }

    public async Task<(bool Success, string? Token, string? Message)> LoginAsync(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null)
            return (false, null, "Invalid credentials");

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return (false, null, "Invalid credentials");

        user.LastLoginAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        var token = GenerateJwtToken(user);
        return (true, token, "Login successful");
    }

    public Task<bool> ValidateTokenAsync(string token)
    {
        try
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);
            new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            }, out SecurityToken validatedToken);

            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);
        var expirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"]!);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName ?? user.Email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
