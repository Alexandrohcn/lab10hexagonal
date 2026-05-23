using Lab10_AlexandroCano.Application.DTOs.Responses;
using Lab10_AlexandroCano.Application.Interfaces.UnitOfWork;
using Lab10_AlexandroCano.Application.Mappings;

namespace Lab10_AlexandroCano.Application.UseCases.Responses;

public class GetResponsesByTicketUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetResponsesByTicketUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ResponseDto>> ExecuteAsync(Guid ticketId)
    {
        var responses = await _unitOfWork.Responses.GetByTicketIdAsync(ticketId);
        return responses.Select(response => response.ToDto());
    }
}
