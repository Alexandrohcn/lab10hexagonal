namespace Lab10_AlexandroCano.Application.DTOs.Tickets;

public class CreateTicketDto
{
    public Guid UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }
}