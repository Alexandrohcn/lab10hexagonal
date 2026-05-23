using Lab10_AlexandroCano.Application.DTOs.Responses;
using Lab10_AlexandroCano.Application.Interfaces.UnitOfWork;
using Lab10_AlexandroCano.Application.Mappings;

namespace Lab10_AlexandroCano.Application.UseCases.Responses;

public class GetAllResponsesUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllResponsesUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ResponseDto>> ExecuteAsync()
    {
        var responses = await _unitOfWork.Responses.GetAllAsync();
        return responses.Select(response => response.ToDto());
    }
}
