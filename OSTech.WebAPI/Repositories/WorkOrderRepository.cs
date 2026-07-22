using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.WorkOrder;
using OSTech.WebAPI.Repositories.Generic;

namespace OSTech.WebAPI.Repositories
{
    public class WorkOrderRepository : Repository<WorkOrder>, IWorkOrderRepository
    {
        public WorkOrderRepository(AppDbContext context) : base(context) { }
        public async Task<WorkOrder?> Update(WorkOrder workOrder)
        {
           var workOrderDb = await _context.WorkOrders
                .FirstOrDefaultAsync(w => w.WorkOrderId == workOrder.WorkOrderId);

            if (workOrderDb is null)
                return null;

            workOrderDb.SetDescription(workOrder.Description);
            workOrderDb.SetTitle(workOrder.Title);
            workOrderDb.SetAmount(workOrder.Amount);
            workOrderDb.ChangeDeadline(workOrder.Deadline);

            workOrderDb.AssignTechnician(workOrder.TechnicianId);
            workOrderDb.AssignCustomer(workOrder.CustomerId);
            workOrderDb.AssignCategory(workOrder.CategoryId);
            workOrderDb.AssignEquipment(workOrder.EquipmentId);

            return workOrderDb;
        }
    }
}
