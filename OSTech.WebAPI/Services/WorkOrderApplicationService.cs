using OSTech.Domain.Entities;
using OSTech.Domain.Exceptions;
using OSTech.Infrastructure.UnitOfWork;

namespace OSTech.WebAPI.Services
{
    public class WorkOrderApplicationService
    {
        private readonly IUnitOfWork _uof;

        public WorkOrderApplicationService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<WorkOrder> CreateWorkOrderAsync(
            string description, string title, decimal amount,
            DateOnly deadline, DateOnly openingDate,
            int technicianId, int customerId, int categoryId, int equipmentId)
        {
            var technician = await _uof.TechnicianRepository.GetById(t => t.TechnicianId == technicianId);

            if (technician is null)
                throw new DomainException("Técnico não encontrado.");

            var workOrder = new WorkOrder(
                description, title, amount, deadline, openingDate,
                technician, customerId, categoryId, equipmentId);

            await _uof.WorkOrderRepository.Create(workOrder);
            await _uof.CommitAsync();

            return workOrder;
        }
    }
}