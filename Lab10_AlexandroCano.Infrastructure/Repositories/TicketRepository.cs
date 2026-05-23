using Lab10_AlexandroCano.Application.Interfaces.Repositories;
using Lab10_AlexandroCano.Domain.Entities;
using Lab10_AlexandroCano.Infrastructure.Persistence;

namespace Lab10_AlexandroCano.Infrastructure.Repositories;

public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    public TicketRepository(AppDbContext context) : base(context)
    {
    }
}