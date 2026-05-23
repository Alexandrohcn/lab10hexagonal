using Lab10_AlexandroCano.Application.DTOs.Tickets;
using Lab10_AlexandroCano.Application.Interfaces.UnitOfWork;
using Lab10_AlexandroCano.Application.Mappings;
using Lab10_AlexandroCano.Domain.Constants;
using Lab10_AlexandroCano.Domain.Entities;

namespace Lab10_AlexandroCano.Application.UseCases.Tickets;

public class CreateTicketUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTicketUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketDto> ExecuteAsync(CreateTicketDto dto)
    {
        var ticket = new Ticket
        {
            TicketId = Guid.NewGuid(),
            UserId = dto.UserId,
            Title = dto.Title,
            Description = dto.Description,
            Status = TicketStatus.Abierto,
            CreatedAt = DateTime.Now
        };

        await _unitOfWork.Tickets.AddAsync(ticket);
        await _unitOfWork.SaveChangesAsync();

        return ticket.ToDto();
    }
}
