using MediatR;
using Microsoft.EntityFrameworkCore;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.WorkOrder;

namespace OSTech.WebAPI.Queries.WorkOrders;

public class GetWorkOrderByIdHandler : IRequestHandler<GetWorkOrderByIdQuery, WorkOrderDTO?>
{
    private readonly AppDbContext _context;

    public GetWorkOrderByIdHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<WorkOrderDTO> Handle(GetWorkOrderByIdQuery request, CancellationToken cancellationToken)
    {

        var dto = await _context.WorkOrders
        .Where(w => w.WorkOrderId == request.WorkOrderId)
        .Select(w => new WorkOrderDTO
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
        })
        .FirstOrDefaultAsync();

        return dto;
    }
}
