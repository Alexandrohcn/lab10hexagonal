using Lab10_AlexandroCano.Application.DTOs.Responses;
using Lab10_AlexandroCano.Domain.Entities;

namespace Lab10_AlexandroCano.Application.Mappings;

public static class ResponseMapping
{
    public static ResponseDto ToDto(this Response response)
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
