using Lab10_AlexandroCano.Application.DTOs.Responses;
using Lab10_AlexandroCano.Application.Interfaces.UnitOfWork;
using Lab10_AlexandroCano.Application.Mappings;

namespace Lab10_AlexandroCano.Application.UseCases.Responses;

public class GetResponseByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetResponseByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseDto?> ExecuteAsync(Guid id)
    {
        var response = await _unitOfWork.Responses.GetByIdAsync(id);
        return response?.ToDto();
    }
}
