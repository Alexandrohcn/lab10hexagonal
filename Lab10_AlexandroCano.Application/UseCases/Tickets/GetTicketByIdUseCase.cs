using Lab10_AlexandroCano.Application.DTOs.Tickets;
using Lab10_AlexandroCano.Application.Interfaces.UnitOfWork;
using Lab10_AlexandroCano.Application.Mappings;

namespace Lab10_AlexandroCano.Application.UseCases.Tickets;

public class GetTicketByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTicketByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketDto?> ExecuteAsync(Guid id)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
        return ticket?.ToDto();
    }
}
