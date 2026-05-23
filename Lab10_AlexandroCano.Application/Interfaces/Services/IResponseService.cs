using Lab10_AlexandroCano.Application.DTOs.Responses;

namespace Lab10_AlexandroCano.Application.Interfaces.Services;

public interface IResponseService
{
    Task<IEnumerable<ResponseDto>> GetAllAsync();

    Task<ResponseDto?> GetByIdAsync(Guid id);

    Task<IEnumerable<ResponseDto>> GetByTicketIdAsync(Guid ticketId);

    Task<ResponseDto> CreateAsync(CreateResponseDto dto);
}