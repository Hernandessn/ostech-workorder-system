using MediatR;
using OSTech.Domain.Entities;

namespace OSTech.WebAPI.Commands.WorkOrders;

public class CancelWorkOrderCommand : IRequest<WorkOrder>
{
    public int WorkOrderId { get; set; }
}
