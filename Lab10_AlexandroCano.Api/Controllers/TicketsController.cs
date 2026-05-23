using Lab10_AlexandroCano.Application.DTOs;
using Lab10_AlexandroCano.Application.Interfaces;
using Lab10_AlexandroCano.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_AlexandroCano.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public TicketsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tickets = await _unitOfWork.Tickets.GetAllAsync();

        return Ok(tickets);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);

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
        var ticket = new Ticket
        {
            TicketId = Guid.NewGuid(),
            UserId = dto.UserId,
            Title = dto.Title,
            Description = dto.Description,
            Status = "abierto",
            CreatedAt = DateTime.Now
        };

        await _unitOfWork.Tickets.AddAsync(ticket);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            Message = "Ticket creado correctamente",
            Ticket = ticket
        });
    }

    [HttpPut("{id:guid}/status")]
    [Authorize(Roles = "Admin,Support")]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateTicketStatusDto dto)
    {
        var validStatuses = new[] { "abierto", "en_proceso", "cerrado" };

        if (!validStatuses.Contains(dto.Status))
        {
            return BadRequest(new
            {
                Message = "Estado no válido. Use: abierto, en_proceso o cerrado."
            });
        }

        var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);

        if (ticket is null)
        {
            return NotFound(new
            {
                Message = "Ticket no encontrado"
            });
        }

        ticket.Status = dto.Status;
        ticket.ClosedAt = dto.Status == "cerrado" ? DateTime.Now : null;

        _unitOfWork.Tickets.Update(ticket);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            Message = "Estado del ticket actualizado correctamente",
            Ticket = ticket
        });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);

        if (ticket is null)
        {
            return NotFound(new
            {
                Message = "Ticket no encontrado"
            });
        }

        _unitOfWork.Tickets.Delete(ticket);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            Message = "Ticket eliminado correctamente"
        });
    }
}