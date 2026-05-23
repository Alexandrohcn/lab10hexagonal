using Lab10_AlexandroCano.Application.DTOs.Tickets;
using Lab10_AlexandroCano.Application.UseCases.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_AlexandroCano.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly GetAllTicketsUseCase _getAllTicketsUseCase;
    private readonly GetTicketByIdUseCase _getTicketByIdUseCase;
    private readonly CreateTicketUseCase _createTicketUseCase;
    private readonly UpdateTicketStatusUseCase _updateTicketStatusUseCase;
    private readonly DeleteTicketUseCase _deleteTicketUseCase;

    public TicketsController(
        GetAllTicketsUseCase getAllTicketsUseCase,
        GetTicketByIdUseCase getTicketByIdUseCase,
        CreateTicketUseCase createTicketUseCase,
        UpdateTicketStatusUseCase updateTicketStatusUseCase,
        DeleteTicketUseCase deleteTicketUseCase)
    {
        _getAllTicketsUseCase = getAllTicketsUseCase;
        _getTicketByIdUseCase = getTicketByIdUseCase;
        _createTicketUseCase = createTicketUseCase;
        _updateTicketStatusUseCase = updateTicketStatusUseCase;
        _deleteTicketUseCase = deleteTicketUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tickets = await _getAllTicketsUseCase.ExecuteAsync();

        return Ok(tickets);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var ticket = await _getTicketByIdUseCase.ExecuteAsync(id);

        if (ticket is null)
        {
            return NotFound(new
            {
                Message = "Ticket no encontrado"
            });
        }

        return Ok(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTicketDto dto)
    {
        var ticket = await _createTicketUseCase.ExecuteAsync(dto);

        return Ok(new
        {
            Message = "Ticket creado correctamente",
            Ticket = ticket
        });
    }

    [HttpPut("{id:guid}/status")]
    [Authorize(Roles = "Admin,Support")]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        UpdateTicketStatusDto dto)
    {
        var result = await _updateTicketStatusUseCase.ExecuteAsync(id, dto);

        if (!result.Success)
        {
            if (result.Error == "Ticket no encontrado.")
            {
                return NotFound(new
                {
                    Message = result.Error
                });
            }

            return BadRequest(new
            {
                Message = result.Error
            });
        }

        return Ok(new
        {
            Message = "Estado del ticket actualizado correctamente",
            Ticket = result.Value
        });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _deleteTicketUseCase.ExecuteAsync(id);

        if (!result.Success)
        {
            return NotFound(new
            {
                Message = result.Error
            });
        }

        return Ok(new
        {
            Message = "Ticket eliminado correctamente"
        });
    }
}
