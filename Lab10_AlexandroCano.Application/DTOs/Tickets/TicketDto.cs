namespace Lab10_AlexandroCano.Application.DTOs.Tickets;

public class TicketDto
{
    public Guid TicketId { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime? CreatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }
}