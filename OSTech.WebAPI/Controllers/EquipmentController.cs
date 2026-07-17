using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.Equipment;
using OSTech.WebAPI.Dtos.WorkOrder;

namespace OSTech.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EquipmentController : Controller
    {
            private readonly AppDbContext _context;

            public EquipmentController(AppDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public ActionResult<IEnumerable<EquipmentDTO>> Get()
            {
                try
                {
                    var equipments = _context.Equipments
                                             .AsNoTracking()
                                             .Select(t => new EquipmentDTO
                                             {
                                                 EquipmentId = t.EquipmentId,
                                                 Name = t.Name,
                                                 Brand = t.Brand,
                                                 Model = t.Model,
                                                 SerialNumber = t.SerialNumber
                                             })
                                             .ToList();
                    return Ok(equipments);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                                               "An issue occurred while processing your request.");
                }
            }

            [HttpGet("{id:int}", Name = "GetEquipments")]
            public ActionResult<EquipmentDTO> Get(int id)
            {
                try
                {
                    var equipment = _context.Equipments
                                             .AsNoTracking()
                                             .FirstOrDefault(t => t.EquipmentId == id);

                    if (equipment is null)
                        return NotFound();

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
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                                "An issue occurred while processing your request.");
                }
            }
            [HttpPost]
            public ActionResult<EquipmentDTO> Post(CreateEquipmentDTO dto)
            {
                try
                {
                    var equipment = new Equipment(
                        dto.Name,
                        dto.Brand,
                        dto.Model,
                        dto.SerialNumber
                    );

                    _context.Equipments.Add(equipment);
                    _context.SaveChanges();

                    var equipmentDTO = new EquipmentDTO
                    {
                        EquipmentId = equipment.EquipmentId,
                        Name = equipment.Name,
                        Brand = equipment.Brand,
                        Model = equipment.Model,
                        SerialNumber = equipment.SerialNumber
                    };

                    return CreatedAtRoute(
                        "GetWorkOrder",
                        new { id = equipment.EquipmentId },
                        equipmentDTO);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                                "An issue occurred while processing your request.");
                }
            }
            [HttpPut("{id:int}")]
            public ActionResult<EquipmentDTO> Put(int id, UpdateEquipmentDTO dto)
            {
                try
                {
                    var equipment = _context.Equipments.Find(id);

                    if (equipment is null)
                        return NotFound();

                    equipment.SetName(equipment.Name);
                    equipment.SetBrand(dto.Brand);
                    equipment.SetModel(dto.Model);
                    equipment.SetSerialNumber(dto.SerialNumber);

                    _context.SaveChanges();

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
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                                "An issue occurred while processing your request.");
                }
            }
            [HttpDelete("{id:int}")]
            public ActionResult Delete(int id)
            {
                try
                {
                    var equipment = _context.Equipments.FirstOrDefault(p => p.EquipmentId == id);

                    if (equipment is null)
                        return NotFound();

                    _context.Equipments.Remove(equipment);
                    _context.SaveChanges();

                    return NoContent();
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                                "An issue occurred while processing your request.");
                }
            }
        }
}
