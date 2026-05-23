using Lab10_AlexandroCano.Application.DTOs;
using Lab10_AlexandroCano.Application.Interfaces;
using Lab10_AlexandroCano.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_AlexandroCano.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ResponsesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ResponsesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var responses = await _unitOfWork.Responses.GetAllAsync();

        return Ok(responses);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _unitOfWork.Responses.GetByIdAsync(id);

        if (response is null)
        {
            return NotFound(new
            {
                Message = "Respuesta no encontrada"
            });
        }

        return Ok(response);
    }

    [HttpGet("ticket/{ticketId:guid}")]
    public async Task<IActionResult> GetByTicket(Guid ticketId)
    {
        var responses = await _unitOfWork.Responses.GetAllAsync();

        var filteredResponses = responses
            .Where(r => r.TicketId == ticketId)
            .ToList();

        return Ok(filteredResponses);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Support")]
    public async Task<IActionResult> Create(CreateResponseDto dto)
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

        return Ok(new
        {
            Message = "Respuesta registrada correctamente",
            Response = response
        });
    }
}