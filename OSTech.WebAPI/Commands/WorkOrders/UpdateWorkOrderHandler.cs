using MediatR;
using OSTech.Domain.Entities;
using OSTech.Domain.Exceptions;
using OSTech.Infrastructure.UnitOfWork;

namespace OSTech.WebAPI.Commands.WorkOrders;

public class UpdateWorkOrderHandler : IRequestHandler<UpdateWorkOrderCommand, WorkOrder>
{
    private readonly IUnitOfWork _uof;

    public UpdateWorkOrderHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }
    public async Task<WorkOrder> Handle(UpdateWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var workOrder = await _uof.WorkOrderRepository.GetById(c => c.WorkOrderId == request.WorkOrderId);

        if (workOrder is null)
        {
            throw new DomainException("WorkOrder not found.");
        }
        else
        {
            var technician = await _uof.TechnicianRepository.GetById(c => c.TechnicianId == request.TechnicianId);
            workOrder.AssignTechnician(technician);
        }

        workOrder.SetDescription(request.Description);
        workOrder.SetTitle(request.Title);
        workOrder.SetAmount(request.Amount);
        workOrder.ChangeDeadline(request.Deadline);

        workOrder.AssignCustomer(request.CustomerId);
        workOrder.AssignCategory(request.CategoryId);
        workOrder.AssignEquipment(request.EquipmentId);

        await _uof.WorkOrderRepository.Update(workOrder);
        await _uof.CommitAsync();

        return workOrder;
    }
}
