using Lab10_AlexandroCano.Domain.Entities;

namespace Lab10_AlexandroCano.Application.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUsernameWithRolesAsync(string username);
}