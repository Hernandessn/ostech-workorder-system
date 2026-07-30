using OSTech.Domain.Entities;
using OSTech.WebAPI.Repositories.Generic;

namespace OSTech.WebAPI.Repositories
{
    public interface IEquipmentRepository : IRepository<Equipment>
    {
        Task<Equipment?> Update(Equipment equipment);
    }
}
