using Lab10_AlexandroCano.Application.DTOs.Tickets;
using Lab10_AlexandroCano.Domain.Entities;

namespace Lab10_AlexandroCano.Application.Mappings;

public static class TicketMapping
{
    public static TicketDto ToDto(this Ticket ticket)
    {
        return new TicketDto
        {
            TicketId = ticket.TicketId,
            UserId = ticket.UserId,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status,
            CreatedAt = ticket.CreatedAt,
            ClosedAt = ticket.ClosedAt
        };
    }
}
