using Lab10_AlexandroCano.Application.Interfaces.Repositories;
using Lab10_AlexandroCano.Domain.Entities;
using Lab10_AlexandroCano.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lab10_AlexandroCano.Infrastructure.Repositories;

public class ResponseRepository : GenericRepository<Response>, IResponseRepository
{
    public ResponseRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Response>> GetByTicketIdAsync(Guid ticketId)
    {
        return await Context.Responses
            .Where(r => r.TicketId == ticketId)
            .ToListAsync();
    }
}