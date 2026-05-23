using Lab10_AlexandroCano.Application.DTOs.Tickets;
using Lab10_AlexandroCano.Application.Interfaces.UnitOfWork;
using Lab10_AlexandroCano.Application.Mappings;

namespace Lab10_AlexandroCano.Application.UseCases.Tickets;

public class GetAllTicketsUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTicketsUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TicketDto>> ExecuteAsync()
    {
        var tickets = await _unitOfWork.Tickets.GetAllAsync();
        return tickets.Select(ticket => ticket.ToDto());
    }
}
