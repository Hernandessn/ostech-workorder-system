using OSTech.WebMVC.Models;

namespace OSTech.WebMVC.Services
{
    public interface IEquipmentService
    {
        Task<IEnumerable<EquipmentViewModel>> GetEquipmentsAsync();
        Task<EquipmentViewModel> GetEquipmentByIdAsync(int id);
        Task<EquipmentViewModel> CreateEquipment(EquipmentViewModel equipmentVM);
        Task<bool> UpdateEquipmentAsync(int id, EquipmentViewModel equipmentVM);
        Task<bool> DeleteEquipment(int id);
    }
}
