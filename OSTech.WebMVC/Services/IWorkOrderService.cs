using OSTech.WebMVC.Models;

namespace OSTech.WebMVC.Services
{
    public interface IWorkOrderService
    {
        Task<IEnumerable<WorkOrderViewModel>> GetWorkOrdersAsync();
        Task<WorkOrderViewModel> GetWorkOrderByIdAsync(int id);
        Task<WorkOrderViewModel> CreateWorkOrder(WorkOrderViewModel workOrderVM);
        Task<bool> UpdateWorkOrderAsync(int id, WorkOrderViewModel workOrderVM);
        Task<bool> DeleteWorkOrder(int id);
    }
}
