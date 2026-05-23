namespace Lab10_AlexandroCano.Application.DTOs.Auth;

public class LoginResponseDto
{
    public string Message { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public List<string> Roles { get; set; } = new();

    public string Token { get; set; } = string.Empty;
}