using Lab10_AlexandroCano.Application.DTOs.Auth;

namespace Lab10_AlexandroCano.Application.Interfaces.Services;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginDto);
}