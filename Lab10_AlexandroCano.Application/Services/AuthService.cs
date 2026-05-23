using Lab10_AlexandroCano.Application.DTOs.Auth;
using Lab10_AlexandroCano.Application.Interfaces;
using Lab10_AlexandroCano.Application.Interfaces.Services;

namespace Lab10_AlexandroCano.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUnitOfWork unitOfWork,
        ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginDto)
    {
        var user = await _unitOfWork.Users
            .GetByUsernameWithRolesAsync(loginDto.Username);

        if (user is null)
        {
            return null;
        }

        if (user.PasswordHash != loginDto.Password)
        {
            return null;
        }

        var roles = user.UserRoles
            .Select(ur => ur.Role.RoleName)
            .Where(role => !string.IsNullOrWhiteSpace(role))
            .ToList();

        var token = _tokenService.GenerateToken(user, roles);

        return new LoginResponseDto
        {
            Message = "Login correcto",
            UserId = user.UserId,
            Username = user.Username,
            Roles = roles,
            Token = token
        };
    }
}