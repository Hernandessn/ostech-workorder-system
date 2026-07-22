using OSTech.Domain.Entities;
using OSTech.WebAPI.Repositories.Generic;

namespace OSTech.WebAPI.Repositories
{
    public interface IWorkOrderRepository : IRepository<WorkOrder>
    {
        Task<WorkOrder?> Update(WorkOrder workOrder);
    }
}
