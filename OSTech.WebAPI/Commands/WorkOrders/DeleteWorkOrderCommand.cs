using MediatR;
using OSTech.Domain.Exceptions;
using OSTech.Infrastructure.UnitOfWork;

public class DeleteWorkOrderCommand : IRequest<Unit>
{
    public int WorkOrderId { get; set; }
}

public class DeleteWorkOrderHandler : IRequestHandler<DeleteWorkOrderCommand, Unit>
{
    private readonly IUnitOfWork _uof;

    public DeleteWorkOrderHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Unit> Handle(DeleteWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var workOrder = await _uof.WorkOrderRepository.GetById(w => w.WorkOrderId == request.WorkOrderId);

        if (workOrder is null)
            throw new DomainException("WorkOrder not found.");

        await _uof.WorkOrderRepository.Delete(request.WorkOrderId);
        await _uof.CommitAsync();

        return Unit.Value;
    }
}