using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lab10_AlexandroCano.Application.DTOs;
using Lab10_AlexandroCano.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Lab10_AlexandroCano.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthController(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = await _unitOfWork.Users.GetByUsernameWithRolesAsync(loginDto.Username);

        if (user is null || user.PasswordHash != loginDto.Password)
        {
            return Unauthorized(new
            {
                Message = "Credenciales incorrectas"
            });
        }

        var roles = user.UserRoles
            .Select(ur => ur.Role?.RoleName)
            .Where(role => role is not null)
            .Select(role => role!)
            .ToList();

        var token = GenerateJwtToken(user.Username, user.UserId, roles);

        return Ok(new
        {
            Message = "Login correcto",
            UserId = user.UserId,
            Username = user.Username,
            Roles = roles,
            Token = token
        });
    }

    private string GenerateJwtToken(string username, Guid userId, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, username)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}