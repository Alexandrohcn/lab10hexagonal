using Lab10_AlexandroCano.Domain.Entities;

namespace Lab10_AlexandroCano.Application.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }

    IGenericRepository<Role> Roles { get; }

    IGenericRepository<Ticket> Tickets { get; }

    IGenericRepository<Response> Responses { get; }

    Task<int> SaveChangesAsync();
}