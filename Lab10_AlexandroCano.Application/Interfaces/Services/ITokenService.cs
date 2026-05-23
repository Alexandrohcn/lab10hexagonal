using Lab10_AlexandroCano.Domain.Entities;

namespace Lab10_AlexandroCano.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user, IEnumerable<string> roles);
}