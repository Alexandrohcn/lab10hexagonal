using Lab10_AlexandroCano.Application.DTOs.Auth;
using Lab10_AlexandroCano.Application.Interfaces.Services;
using Lab10_AlexandroCano.Application.UseCases.Auth;

namespace Lab10_AlexandroCano.Application.Services;

public class AuthService : IAuthService
{
    private readonly LoginUseCase _loginUseCase;

    public AuthService(LoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginDto)
    {
        var result = await _loginUseCase.ExecuteAsync(loginDto);
        return result.Success ? result.Value : null;
    }
}
