using Lab10_AlexandroCano.Application.DTOs.Auth;
using Lab10_AlexandroCano.Application.Interfaces.Repositories;
using Lab10_AlexandroCano.Application.Interfaces.Services;

namespace Lab10_AlexandroCano.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUserRepository userRepository,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);

        if (user == null)
            return null;

        if (user.Password != request.Password)
            return null;

        var token = _tokenService.GenerateToken(user);

        return new LoginResponseDto
        {
            Username = user.Username,
            Role = user.Role,
            Token = token
        };
    }
}