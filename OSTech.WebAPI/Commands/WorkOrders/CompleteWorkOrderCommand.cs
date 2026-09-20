using MediatR;
using OSTech.Domain.Entities;

namespace OSTech.WebAPI.Commands.WorkOrders;

public class CompleteWorkOrderCommand : IRequest<WorkOrder>
{
    public int WorkOrderId { get; set; }
}
