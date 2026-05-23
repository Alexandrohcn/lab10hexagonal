using Lab10_AlexandroCano.Application.Interfaces;
using Lab10_AlexandroCano.Domain.Entities;
using Lab10_AlexandroCano.Infrastructure.Persistence;

namespace Lab10_AlexandroCano.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IUserRepository Users { get; }

    public IGenericRepository<Role> Roles { get; }

    public IGenericRepository<Ticket> Tickets { get; }

    public IGenericRepository<Response> Responses { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        Users = new UserRepository(_context);
        Roles = new GenericRepository<Role>(_context);
        Tickets = new GenericRepository<Ticket>(_context);
        Responses = new GenericRepository<Response>(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}