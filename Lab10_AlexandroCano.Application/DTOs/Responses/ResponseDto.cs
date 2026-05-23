namespace Lab10_AlexandroCano.Application.DTOs.Responses;

public class ResponseDto
{
    public Guid ResponseId { get; set; }

    public Guid TicketId { get; set; }

    public Guid ResponderId { get; set; }

    public string Message { get; set; } = string.Empty;

    public DateTime? CreatedAt { get; set; }
}