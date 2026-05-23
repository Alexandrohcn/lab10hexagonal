using Lab10_AlexandroCano.Application.DTOs.Tickets;

namespace Lab10_AlexandroCano.Application.Interfaces.Services;

public interface ITicketService
{
    Task<IEnumerable<TicketDto>> GetAllAsync();

    Task<TicketDto?> GetByIdAsync(Guid id);

    Task<TicketDto> CreateAsync(CreateTicketDto dto);

    Task<(bool Success, string? Error, TicketDto? Ticket)> UpdateStatusAsync(
        Guid id,
        UpdateTicketStatusDto dto);

    Task<bool> DeleteAsync(Guid id);
}