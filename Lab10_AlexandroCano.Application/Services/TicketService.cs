using Lab10_AlexandroCano.Application.DTOs.Tickets;
using Lab10_AlexandroCano.Application.Interfaces.Services;
using Lab10_AlexandroCano.Application.UseCases.Tickets;

namespace Lab10_AlexandroCano.Application.Services;

public class TicketService : ITicketService
{
    private readonly GetAllTicketsUseCase _getAllTicketsUseCase;
    private readonly GetTicketByIdUseCase _getTicketByIdUseCase;
    private readonly CreateTicketUseCase _createTicketUseCase;
    private readonly UpdateTicketStatusUseCase _updateTicketStatusUseCase;
    private readonly DeleteTicketUseCase _deleteTicketUseCase;

    public TicketService(
        GetAllTicketsUseCase getAllTicketsUseCase,
        GetTicketByIdUseCase getTicketByIdUseCase,
        CreateTicketUseCase createTicketUseCase,
        UpdateTicketStatusUseCase updateTicketStatusUseCase,
        DeleteTicketUseCase deleteTicketUseCase)
    {
        _getAllTicketsUseCase = getAllTicketsUseCase;
        _getTicketByIdUseCase = getTicketByIdUseCase;
        _createTicketUseCase = createTicketUseCase;
        _updateTicketStatusUseCase = updateTicketStatusUseCase;
        _deleteTicketUseCase = deleteTicketUseCase;
    }

    public async Task<IEnumerable<TicketDto>> GetAllAsync()
    {
        return await _getAllTicketsUseCase.ExecuteAsync();
    }

    public async Task<TicketDto?> GetByIdAsync(Guid id)
    {
        return await _getTicketByIdUseCase.ExecuteAsync(id);
    }

    public async Task<TicketDto> CreateAsync(CreateTicketDto dto)
    {
        return await _createTicketUseCase.ExecuteAsync(dto);
    }

    public async Task<(bool Success, string? Error, TicketDto? Ticket)> UpdateStatusAsync(
        Guid id,
        UpdateTicketStatusDto dto)
    {
        var result = await _updateTicketStatusUseCase.ExecuteAsync(id, dto);
        return (result.Success, result.Error, result.Value);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _deleteTicketUseCase.ExecuteAsync(id);
        return result.Success;
    }
}
