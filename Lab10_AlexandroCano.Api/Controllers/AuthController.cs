using Lab10_AlexandroCano.Application.DTOs.Auth;
using Lab10_AlexandroCano.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_AlexandroCano.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequestDto loginDto)
    {
        var response = await _authService.LoginAsync(loginDto);

        if (response is null)
        {
            return Unauthorized(new
            {
                Message = "Credenciales incorrectas"
            });
        }

        return Ok(response);
    }
}