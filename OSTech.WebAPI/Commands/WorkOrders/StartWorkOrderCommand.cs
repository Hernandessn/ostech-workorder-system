using MediatR;
using OSTech.Domain.Entities;

namespace OSTech.WebAPI.Commands.WorkOrders;

public class StartWorkOrderCommand : IRequest<WorkOrder>
{
    public int WorkOrderId { get; set; }
}
