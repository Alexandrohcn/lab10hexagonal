using Lab10_AlexandroCano.Application.Interfaces.Repositories;
using Lab10_AlexandroCano.Domain.Entities;
using Lab10_AlexandroCano.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lab10_AlexandroCano.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByUsernameWithRolesAsync(string username)
    {
        return await Context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Username == username);
    }
}