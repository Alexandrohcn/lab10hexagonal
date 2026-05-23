using Lab10_AlexandroCano.Domain.Entities;

namespace Lab10_AlexandroCano.Application.Interfaces.Security;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<string> roles);
}
