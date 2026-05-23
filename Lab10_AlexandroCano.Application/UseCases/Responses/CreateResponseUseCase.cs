using Lab10_AlexandroCano.Application.DTOs.Responses;
using Lab10_AlexandroCano.Application.Interfaces.UnitOfWork;
using Lab10_AlexandroCano.Application.Mappings;
using Lab10_AlexandroCano.Domain.Entities;

namespace Lab10_AlexandroCano.Application.UseCases.Responses;

public class CreateResponseUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateResponseUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseDto> ExecuteAsync(CreateResponseDto dto)
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

        return response.ToDto();
    }
}
