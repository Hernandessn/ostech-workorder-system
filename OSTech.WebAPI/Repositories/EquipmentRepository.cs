using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.Equipment;
using OSTech.WebAPI.Repositories.Generic;

namespace OSTech.WebAPI.Repositories
{
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<Equipment?> Update(Equipment equipment)
        {
            var equipmentDb = await _context.Equipments
                .FirstOrDefaultAsync(t => t.EquipmentId == equipment.EquipmentId);

            equipmentDb.SetName(equipment.Name);
            equipmentDb.SetBrand(equipment.Brand);
            equipmentDb.SetModel(equipment.Model);
            equipmentDb.SetSerialNumber(equipment.SerialNumber);

            return equipmentDb;
        }
    }
}
