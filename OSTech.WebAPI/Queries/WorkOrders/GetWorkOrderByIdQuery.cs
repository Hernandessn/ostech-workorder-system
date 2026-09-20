using MediatR;
using OSTech.WebAPI.Dtos.WorkOrder;

namespace OSTech.WebAPI.Queries.WorkOrders;

public class GetWorkOrderByIdQuery : IRequest<WorkOrderDTO?>
{
    public int WorkOrderId { get; set; }
}
