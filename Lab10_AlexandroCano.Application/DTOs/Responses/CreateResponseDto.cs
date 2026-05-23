namespace Lab10_AlexandroCano.Application.DTOs.Responses;

public class CreateResponseDto
{
    public Guid TicketId { get; set; }

    public Guid ResponderId { get; set; }

    public string Message { get; set; } = string.Empty;
}