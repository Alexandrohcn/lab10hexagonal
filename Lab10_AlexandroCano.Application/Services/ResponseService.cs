using Lab10_AlexandroCano.Application.DTOs.Responses;
using Lab10_AlexandroCano.Application.Interfaces;
using Lab10_AlexandroCano.Application.Interfaces.Services;
using Lab10_AlexandroCano.Domain.Entities;

namespace Lab10_AlexandroCano.Application.Services;

public class ResponseService : IResponseService
{
    private readonly IUnitOfWork _unitOfWork;

    public ResponseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ResponseDto>> GetAllAsync()
    {
        var responses = await _unitOfWork.Responses.GetAllAsync();
        return responses.Select(MapToDto);
    }

    public async Task<ResponseDto?> GetByIdAsync(Guid id)
    {
        var response = await _unitOfWork.Responses.GetByIdAsync(id);
        return response is null ? null : MapToDto(response);
    }

    public async Task<IEnumerable<ResponseDto>> GetByTicketIdAsync(Guid ticketId)
    {
        var responses = await _unitOfWork.Responses.GetByTicketIdAsync(ticketId);
        return responses.Select(MapToDto);
    }

    public async Task<ResponseDto> CreateAsync(CreateResponseDto dto)
    {
        var response = new Response
        {
            ResponseId = Guid.NewGuid(),
            TicketId = dto.TicketId,
            ResponderId = dto.ResponderId,
            Message = dto.Message,
            CreatedAt = DateTime.Now
        };

        await _unitOfWork.Responses.AddAsync(response);
        await _unitOfWork.SaveChangesAsync();

        return MapToDto(response);
    }

    private static ResponseDto MapToDto(Response response)
    {
        return new ResponseDto
        {
            ResponseId = response.ResponseId,
            TicketId = response.TicketId,
            ResponderId = response.ResponderId,
            Message = response.Message,
            CreatedAt = response.CreatedAt
        };
    }
}