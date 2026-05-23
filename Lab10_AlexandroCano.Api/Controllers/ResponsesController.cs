using Lab10_AlexandroCano.Application.DTOs.Responses;
using Lab10_AlexandroCano.Application.UseCases.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_AlexandroCano.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ResponsesController : ControllerBase
{
    private readonly GetAllResponsesUseCase _getAllResponsesUseCase;
    private readonly GetResponseByIdUseCase _getResponseByIdUseCase;
    private readonly GetResponsesByTicketUseCase _getResponsesByTicketUseCase;
    private readonly CreateResponseUseCase _createResponseUseCase;

    public ResponsesController(
        GetAllResponsesUseCase getAllResponsesUseCase,
        GetResponseByIdUseCase getResponseByIdUseCase,
        GetResponsesByTicketUseCase getResponsesByTicketUseCase,
        CreateResponseUseCase createResponseUseCase)
    {
        _getAllResponsesUseCase = getAllResponsesUseCase;
        _getResponseByIdUseCase = getResponseByIdUseCase;
        _getResponsesByTicketUseCase = getResponsesByTicketUseCase;
        _createResponseUseCase = createResponseUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var responses = await _getAllResponsesUseCase.ExecuteAsync();

        return Ok(responses);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _getResponseByIdUseCase.ExecuteAsync(id);

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
        var responses = await _getResponsesByTicketUseCase.ExecuteAsync(ticketId);

        return Ok(responses);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Support")]
    public async Task<IActionResult> Create(CreateResponseDto dto)
    {
        var response = await _createResponseUseCase.ExecuteAsync(dto);

        return Ok(new
        {
            Message = "Respuesta registrada correctamente",
            Response = response
        });
    }
}
