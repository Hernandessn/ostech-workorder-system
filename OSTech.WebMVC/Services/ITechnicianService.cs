using OSTech.WebMVC.Models;

namespace OSTech.WebMVC.Services
{
    public interface ITechnicianService
    {
        Task<IEnumerable<TechnicianViewModel>> GetTechniciansAsync();
        Task<TechnicianViewModel> GetTechnicianByIdAsync(int id);
        Task<TechnicianViewModel> CreateTechnician(TechnicianViewModel technicianVM);
        Task<bool> UpdateTechnicianAsync(int id, TechnicianViewModel technicianVM);
        Task<bool> DeleteTechnician(int id);
    }
}
