using Lab10_AlexandroCano.Application.Interfaces.Repositories;
using Lab10_AlexandroCano.Application.Interfaces.UnitOfWork;
using Lab10_AlexandroCano.Domain.Entities;
using Lab10_AlexandroCano.Infrastructure.Persistence;
using Lab10_AlexandroCano.Infrastructure.Repositories;

namespace Lab10_AlexandroCano.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    private IUserRepository? _users;
    private IGenericRepository<Role>? _roles;
    private ITicketRepository? _tickets;
    private IResponseRepository? _responses;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IUserRepository Users => _users ??= new UserRepository(_context);

    public IGenericRepository<Role> Roles => _roles ??= new GenericRepository<Role>(_context);

    public ITicketRepository Tickets => _tickets ??= new TicketRepository(_context);

    public IResponseRepository Responses => _responses ??= new ResponseRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
