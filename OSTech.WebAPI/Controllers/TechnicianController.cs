using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.Technician;

namespace OSTech.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TechnicianController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TechnicianController> _logger;
        public TechnicianController(AppDbContext context, ILogger<TechnicianController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TechnicianDTO>>> Get()
        {

            var technicians = await _context.Technicians
                .AsNoTracking()
                .Select(t => new TechnicianDTO
                {
                    TechnicianId = t.TechnicianId,
                    Name = t.Name,
                    Specialty = t.Specialty,
                    Contact = t.Contact,
                    Availability = t.Availability,
                    HiringDate = t.HiringDate
                })
                .ToListAsync();

            return Ok(technicians);

        }

        [HttpGet("{id:int:min(1)}", Name = "GetTechnician")]
        public async Task<ActionResult<TechnicianDTO>> Get(int id)
        {

            var technician = await _context.Technicians
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(t => t.TechnicianId == id);

            if (technician is null)
            {
                _logger.LogWarning($"Technician with id= {id} not found...");
                return NotFound("Technician not found.");
            }

            var dto = new TechnicianDTO
            {
                TechnicianId = technician.TechnicianId,
                Name = technician.Name,
                Specialty = technician.Specialty,
                Contact = technician.Contact,
                Availability = technician.Availability,
                HiringDate = technician.HiringDate
            };

            return Ok(dto);

        }

        [HttpPost]
        public async Task<ActionResult<TechnicianDTO>> Post(CreateTechnicianDTO dto)
        {

            var technician = new Technician(
                dto.Name,
                dto.Specialty,
                dto.Contact,
                dto.Availability,
                dto.HiringDate
                );

            await _context.Technicians.AddAsync(technician);
            await _context.SaveChangesAsync();

            var technicianDto = new TechnicianDTO
            {
                TechnicianId = technician.TechnicianId,
                Name = technician.Name,
                Specialty = technician.Specialty,
                Contact = technician.Contact,
                Availability = technician.Availability,
                HiringDate = technician.HiringDate
            };

            return CreatedAtRoute(
                "GetTechnician",
                new { id = technician.TechnicianId },
                technicianDto);


        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<TechnicianDTO>> Put(int id, UpdateTechnicianDTO dto)
        {

            var technician = await _context.Technicians.FindAsync(id);

            if (technician is null)
            {
                _logger.LogWarning($"Technician with id= {id} not found...");
                return NotFound("Technician not found.");
            }

            technician.SetName(dto.Name);
            technician.SetSpecialty(dto.Specialty);
            technician.SetContact(dto.Contact);
            technician.SetAvailability(dto.Availability);
            technician.SetHiringDate(dto.HiringDate);

            await _context.SaveChangesAsync();

            var technicianDto = new TechnicianDTO
            {
                TechnicianId = technician.TechnicianId,
                Name = technician.Name,
                Specialty = technician.Specialty,
                Contact = technician.Contact,
                Availability = technician.Availability,
                HiringDate = technician.HiringDate
            };

            return Ok(technicianDto);


        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {

            var technician = await _context.Technicians.FirstOrDefaultAsync(p => p.TechnicianId == id);

            if (technician is null)
            {
                _logger.LogWarning($"Technician with id= {id} not found...");
                return NotFound("Technician not found.");
            }

            _context.Technicians.Remove(technician);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
