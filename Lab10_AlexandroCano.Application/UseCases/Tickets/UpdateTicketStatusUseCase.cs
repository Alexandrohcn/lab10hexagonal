using Lab10_AlexandroCano.Application.Common;
using Lab10_AlexandroCano.Application.DTOs.Tickets;
using Lab10_AlexandroCano.Application.Interfaces.UnitOfWork;
using Lab10_AlexandroCano.Application.Mappings;
using Lab10_AlexandroCano.Domain.Constants;

namespace Lab10_AlexandroCano.Application.UseCases.Tickets;

public class UpdateTicketStatusUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTicketStatusUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TicketDto>> ExecuteAsync(Guid id, UpdateTicketStatusDto dto)
    {
        if (!TicketStatus.IsValid(dto.Status))
        {
            return Result<TicketDto>.Fail("Estado no válido. Use: abierto, en_proceso o cerrado.");
        }

        var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);

        if (ticket is null)
        {
            return Result<TicketDto>.Fail("Ticket no encontrado.");
        }

        ticket.Status = dto.Status;
        ticket.ClosedAt = dto.Status == TicketStatus.Cerrado ? DateTime.Now : null;

        _unitOfWork.Tickets.Update(ticket);
        await _unitOfWork.SaveChangesAsync();

        return Result<TicketDto>.Ok(ticket.ToDto());
    }
}
