using OSTech.Domain.Entities;

namespace OSTech.Domain;

public interface IDomainWorkOrderRepository
{
    Task<IEnumerable<WorkOrder>> GetByCategoryId(int categoryId);
}
