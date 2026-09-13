using Microsoft.EntityFrameworkCore;
using OSTech.Domain;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.WorkOrder;
using OSTech.WebAPI.Repositories.Generic;

namespace OSTech.WebAPI.Repositories
{
    public class WorkOrderRepository : Repository<WorkOrder>, IWorkOrderRepository, IDomainWorkOrderRepository
    {
        public WorkOrderRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<WorkOrder>> GetByCategoryId(int categoryId)
        {
            return await _context.WorkOrders
                .Where(w => w.CategoryId == categoryId)
                .ToListAsync();
        }

        public Task<WorkOrder?> Update(WorkOrder workOrder)
        {
            return Task.FromResult(workOrder);
        }
    }
}
