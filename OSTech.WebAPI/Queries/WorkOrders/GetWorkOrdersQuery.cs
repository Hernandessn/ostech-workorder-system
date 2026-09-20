using MediatR;
using OSTech.WebAPI.Dtos.WorkOrder;

namespace OSTech.WebAPI.Queries.WorkOrders;

public class GetWorkOrdersQuery : IRequest<IEnumerable<WorkOrderDTO>>
{
}
