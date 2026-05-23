using Lab10_AlexandroCano.Application.Interfaces.Repositories;
using Lab10_AlexandroCano.Domain.Entities;

namespace Lab10_AlexandroCano.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    IUserRepository Users { get; }

    IGenericRepository<Role> Roles { get; }

    ITicketRepository Tickets { get; }

    IResponseRepository Responses { get; }

    Task<int> SaveChangesAsync();
}
