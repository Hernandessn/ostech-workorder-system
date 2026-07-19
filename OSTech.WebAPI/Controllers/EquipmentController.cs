using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.Equipment;

namespace OSTech.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EquipmentController> _logger;


        public EquipmentController(AppDbContext context, ILogger<EquipmentController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentDTO>>> Get()
        {
            var equipments = await _context.Equipments
                                     .AsNoTracking()
                                     .Select(t => new EquipmentDTO
                                     {
                                         EquipmentId = t.EquipmentId,
                                         Name = t.Name,
                                         Brand = t.Brand,
                                         Model = t.Model,
                                         SerialNumber = t.SerialNumber
                                     })
                                     .ToListAsync();
            return Ok(equipments);

        }

        [HttpGet("{id:int:min(1)}", Name = "GetEquipments")]
        public async Task<ActionResult<EquipmentDTO>> Get(int id)
        {
            var equipment = await _context.Equipments
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(t => t.EquipmentId == id);

            if (equipment is null)
            {
                _logger.LogWarning($"Equipment with id = {id} not found...");
                return NotFound("Equipment not found.");
            }

            var dto = new EquipmentDTO
            {
                EquipmentId = equipment.EquipmentId,
                Name = equipment.Name,
                Brand = equipment.Brand,
                Model = equipment.Model,
                SerialNumber = equipment.SerialNumber
            };

            return Ok(dto);

        }
        [HttpPost]
        public async Task<ActionResult<EquipmentDTO>> Post(CreateEquipmentDTO dto)
        {
            var equipment = new Equipment(
                dto.Name,
                dto.Brand,
                dto.Model,
                dto.SerialNumber
            );

            await _context.Equipments.AddAsync(equipment);
            await _context.SaveChangesAsync();

            var equipmentDTO = new EquipmentDTO
            {
                EquipmentId = equipment.EquipmentId,
                Name = equipment.Name,
                Brand = equipment.Brand,
                Model = equipment.Model,
                SerialNumber = equipment.SerialNumber
            };

            return CreatedAtRoute(
                "GetEquipments",
                new { id = equipment.EquipmentId },
                equipmentDTO);

        }
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<EquipmentDTO>> Put(int id, UpdateEquipmentDTO dto)
        {
            var equipment = await _context.Equipments.FindAsync(id); 

            if (equipment is null)
            {
                _logger.LogWarning($"Equipment with id = {id} not found...");
                return NotFound("Equipment not found.");
            }

            equipment.SetName(dto.Name);
            equipment.SetBrand(dto.Brand);
            equipment.SetModel(dto.Model);
            equipment.SetSerialNumber(dto.SerialNumber);

            await _context.SaveChangesAsync();

            var equipmentDTO = new EquipmentDTO
            {
                EquipmentId = equipment.EquipmentId,
                Name = equipment.Name,
                Brand = equipment.Brand,
                Model = equipment.Model,
                SerialNumber = equipment.SerialNumber
            };

            return Ok(equipmentDTO);

        }
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            var equipment = await _context.Equipments.FindAsync(id);

            if (equipment is null)
            {
                _logger.LogWarning($"Equipment with id = {id} not found...");
                return NotFound("Equipment not found.");
            }

            _context.Equipments.Remove(equipment);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
