using Lab10_AlexandroCano.Application.Common;
using Lab10_AlexandroCano.Application.DTOs.Auth;
using Lab10_AlexandroCano.Application.Interfaces.Security;
using Lab10_AlexandroCano.Application.Interfaces.UnitOfWork;

namespace Lab10_AlexandroCano.Application.UseCases.Auth;

public class LoginUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginUseCase(
        IUnitOfWork unitOfWork,
        IJwtTokenService jwtTokenService)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<LoginResponseDto>> ExecuteAsync(LoginRequestDto loginDto)
    {
        var user = await _unitOfWork.Users.GetByUsernameWithRolesAsync(loginDto.Username);

        if (user is null || user.PasswordHash != loginDto.Password)
        {
            return Result<LoginResponseDto>.Fail("Credenciales incorrectas");
        }

        var roles = user.UserRoles
            .Select(ur => ur.Role.RoleName)
            .Where(role => !string.IsNullOrWhiteSpace(role))
            .ToList();

        var token = _jwtTokenService.GenerateToken(user, roles);

        return Result<LoginResponseDto>.Ok(new LoginResponseDto
        {
            Message = "Login correcto",
            UserId = user.UserId,
            Username = user.Username,
            Roles = roles,
            Token = token
        });
    }
}
