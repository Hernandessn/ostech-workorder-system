using OSTech.Domain;
using OSTech.Domain.Entities;
using OSTech.WebAPI.Repositories;

namespace OSTech.Tests.Helpers
{
    public class WorkOrderRepositoryDomainAdapter : IDomainWorkOrderRepository
    {
        private readonly IWorkOrderRepository _workOrderRepository;

        public WorkOrderRepositoryDomainAdapter(IWorkOrderRepository workOrderRepository)
        {
            _workOrderRepository = workOrderRepository;
        }

        public Task<IEnumerable<WorkOrder>> GetByCategoryId(int categoryId)
        {
            return _workOrderRepository.GetByCategoryId(categoryId);
        }
    }
}