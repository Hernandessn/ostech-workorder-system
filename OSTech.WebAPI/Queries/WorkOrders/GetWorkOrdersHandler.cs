using MediatR;
using Microsoft.EntityFrameworkCore;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.WorkOrder;
using OSTech.WebAPI.Queries.WorkOrders;

public class GetWorkOrdersHandler : IRequestHandler<GetWorkOrdersQuery, IEnumerable<WorkOrderDTO>>
{
    private readonly AppDbContext _context;

    public GetWorkOrdersHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkOrderDTO>> Handle(GetWorkOrdersQuery request, CancellationToken cancellationToken)
    {
        var dtos = await _context.WorkOrders.Select(w => new WorkOrderDTO
        {
            WorkOrderId = w.WorkOrderId,
            Description = w.Description,
            Title = w.Title,
            Status = w.Status,
            Amount = w.Amount,
            Deadline = w.Deadline,
            OpeningDate = w.OpeningDate,
            TechnicianId = w.TechnicianId,
            CustomerId = w.CustomerId,
            CategoryId = w.CategoryId,
            EquipmentId = w.EquipmentId
        }).ToListAsync();

        return dtos;
    }
}