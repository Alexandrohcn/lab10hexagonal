using Lab10_AlexandroCano.Application.DTOs.Responses;
using Lab10_AlexandroCano.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_AlexandroCano.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ResponsesController : ControllerBase
{
    private readonly IResponseService _responseService;

    public ResponsesController(IResponseService responseService)
    {
        _responseService = responseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var responses = await _responseService.GetAllAsync();

        return Ok(responses);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _responseService.GetByIdAsync(id);

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
        var responses = await _responseService.GetByTicketIdAsync(ticketId);

        return Ok(responses);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Support")]
    public async Task<IActionResult> Create(CreateResponseDto dto)
    {
        var response = await _responseService.CreateAsync(dto);

        return Ok(new
        {
            Message = "Respuesta registrada correctamente",
            Response = response
        });
    }
}