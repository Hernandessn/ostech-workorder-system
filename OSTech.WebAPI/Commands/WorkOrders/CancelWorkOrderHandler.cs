using MediatR;
using OSTech.Domain.Entities;
using OSTech.Domain.Exceptions;
using OSTech.Infrastructure.UnitOfWork;

namespace OSTech.WebAPI.Commands.WorkOrders;

public class CancelWorkOrderHandler : IRequestHandler<CancelWorkOrderCommand, WorkOrder>
{
    private readonly IUnitOfWork _uof;

    public CancelWorkOrderHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<WorkOrder> Handle(CancelWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var workOrder = await _uof.WorkOrderRepository.GetById(c => c.WorkOrderId == request.WorkOrderId);

        if (workOrder == null)
            throw new NotFoundDomainException("WorkOrder not found.");

        workOrder.Cancel();

        await _uof.CommitAsync();

        return workOrder;
    }
}
