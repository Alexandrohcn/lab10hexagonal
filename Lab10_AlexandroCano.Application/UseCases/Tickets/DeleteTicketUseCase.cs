using Lab10_AlexandroCano.Application.Common;
using Lab10_AlexandroCano.Application.Interfaces.UnitOfWork;

namespace Lab10_AlexandroCano.Application.UseCases.Tickets;

public class DeleteTicketUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTicketUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> ExecuteAsync(Guid id)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);

        if (ticket is null)
        {
            return Result<bool>.Fail("Ticket no encontrado");
        }

        _unitOfWork.Tickets.Delete(ticket);
        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Ok(true);
    }
}
