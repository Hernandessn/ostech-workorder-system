using MediatR;
using OSTech.Domain.Entities;

namespace OSTech.WebAPI.Commands.WorkOrders;

public class UpdateWorkOrderCommand : IRequest<WorkOrder>
{
    public int WorkOrderId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateOnly Deadline { get; set; }

    public int TechnicianId { get; set; }
    public int CustomerId { get; set; }
    public int CategoryId { get; set; }
    public int EquipmentId { get; set; }
}
