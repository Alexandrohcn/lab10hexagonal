using Lab10_AlexandroCano.Application.DTOs.Auth;
using Lab10_AlexandroCano.Application.UseCases.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_AlexandroCano.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoginUseCase _loginUseCase;

    public AuthController(LoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequestDto loginDto)
    {
        var result = await _loginUseCase.ExecuteAsync(loginDto);

        if (!result.Success)
        {
            return Unauthorized(new
            {
                Message = result.Error
            });
        }

        return Ok(result.Value);
    }
}
