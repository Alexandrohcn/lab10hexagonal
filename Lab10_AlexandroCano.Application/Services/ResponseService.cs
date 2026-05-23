using Lab10_AlexandroCano.Application.DTOs.Responses;
using Lab10_AlexandroCano.Application.Interfaces.Services;
using Lab10_AlexandroCano.Application.UseCases.Responses;

namespace Lab10_AlexandroCano.Application.Services;

public class ResponseService : IResponseService
{
    private readonly GetAllResponsesUseCase _getAllResponsesUseCase;
    private readonly GetResponseByIdUseCase _getResponseByIdUseCase;
    private readonly GetResponsesByTicketUseCase _getResponsesByTicketUseCase;
    private readonly CreateResponseUseCase _createResponseUseCase;

    public ResponseService(
        GetAllResponsesUseCase getAllResponsesUseCase,
        GetResponseByIdUseCase getResponseByIdUseCase,
        GetResponsesByTicketUseCase getResponsesByTicketUseCase,
        CreateResponseUseCase createResponseUseCase)
    {
        _getAllResponsesUseCase = getAllResponsesUseCase;
        _getResponseByIdUseCase = getResponseByIdUseCase;
        _getResponsesByTicketUseCase = getResponsesByTicketUseCase;
        _createResponseUseCase = createResponseUseCase;
    }

    public async Task<IEnumerable<ResponseDto>> GetAllAsync()
    {
        return await _getAllResponsesUseCase.ExecuteAsync();
    }

    public async Task<ResponseDto?> GetByIdAsync(Guid id)
    {
        return await _getResponseByIdUseCase.ExecuteAsync(id);
    }

    public async Task<IEnumerable<ResponseDto>> GetByTicketIdAsync(Guid ticketId)
    {
        return await _getResponsesByTicketUseCase.ExecuteAsync(ticketId);
    }

    public async Task<ResponseDto> CreateAsync(CreateResponseDto dto)
    {
        return await _createResponseUseCase.ExecuteAsync(dto);
    }
}
