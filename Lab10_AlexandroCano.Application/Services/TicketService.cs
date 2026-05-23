using Lab10_AlexandroCano.Application.DTOs.Tickets;
using Lab10_AlexandroCano.Application.Interfaces;
using Lab10_AlexandroCano.Application.Interfaces.Services;
using Lab10_AlexandroCano.Domain.Constants;
using Lab10_AlexandroCano.Domain.Entities;

namespace Lab10_AlexandroCano.Application.Services;

public class TicketService : ITicketService
{
    private readonly IUnitOfWork _unitOfWork;

    public TicketService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TicketDto>> GetAllAsync()
    {
        var tickets = await _unitOfWork.Tickets.GetAllAsync();
        return tickets.Select(MapToDto);
    }

    public async Task<TicketDto?> GetByIdAsync(Guid id)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
        return ticket is null ? null : MapToDto(ticket);
    }

    public async Task<TicketDto> CreateAsync(CreateTicketDto dto)
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

        return MapToDto(ticket);
    }

    public async Task<(bool Success, string? Error, TicketDto? Ticket)> UpdateStatusAsync(
        Guid id,
        UpdateTicketStatusDto dto)
    {
        if (!TicketStatus.IsValid(dto.Status))
        {
            return (false, "Estado no válido. Use: abierto, en_proceso o cerrado.", null);
        }

        var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);

        if (ticket is null)
        {
            return (false, "Ticket no encontrado.", null);
        }

        ticket.Status = dto.Status;
        ticket.ClosedAt = dto.Status == TicketStatus.Cerrado ? DateTime.Now : null;

        _unitOfWork.Tickets.Update(ticket);
        await _unitOfWork.SaveChangesAsync();

        return (true, null, MapToDto(ticket));
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);

        if (ticket is null)
        {
            return false;
        }

        _unitOfWork.Tickets.Delete(ticket);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    private static TicketDto MapToDto(Ticket ticket)
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