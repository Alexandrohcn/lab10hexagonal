using Lab10_AlexandroCano.Application.DTOs.Tickets;
using Lab10_AlexandroCano.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_AlexandroCano.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tickets = await _ticketService.GetAllAsync();

        return Ok(tickets);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var ticket = await _ticketService.GetByIdAsync(id);

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
        var ticket = await _ticketService.CreateAsync(dto);

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
        var result = await _ticketService.UpdateStatusAsync(id, dto);

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
            Ticket = result.Ticket
        });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _ticketService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                Message = "Ticket no encontrado"
            });
        }

        return Ok(new
        {
            Message = "Ticket eliminado correctamente"
        });
    }
}