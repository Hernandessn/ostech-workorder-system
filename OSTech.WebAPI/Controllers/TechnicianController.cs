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

        public TechnicianController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TechnicianDTO>> Get()
        {
            try
            {
                var technicians = _context.Technicians
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
                    .ToList();

                return Ok(technicians);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            "An issue occurred while processing your request.");
            }
        }

        [HttpGet("{id:int}", Name = "GetTechnician")]
        public ActionResult<TechnicianDTO> Get(int id)
        {
            try
            {
                var technician = _context.Technicians
                                         .AsNoTracking()
                                         .FirstOrDefault(t => t.TechnicianId == id);

                if (technician is null)
                    return NotFound();

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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            "An issue occurred while processing your request.");
            }
        }

        [HttpPost]
        public ActionResult<TechnicianDTO> Post(CreateTechnicianDTO dto)
        {
            try
            {
                var technician = new Technician(
                    dto.Name,
                    dto.Specialty,
                    dto.Contact,
                    dto.Availability,
                    dto.HiringDate
                    );

                _context.Technicians.Add(technician);
                _context.SaveChanges();

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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            "An issue occurred while processing your request.");
            }

        }

        [HttpPut("{id:int}")]
        public ActionResult<TechnicianDTO> Put(int id, UpdateTechnicianDTO dto)
        {
            try
            {
                var technician = _context.Technicians.Find(id);

                if (technician is null)
                    return NotFound();

                technician.SetName(dto.Name);
                technician.SetSpecialty(dto.Specialty);
                technician.SetContact(dto.Contact);
                technician.SetAvailability(dto.Availability);
                technician.SetHiringDate(dto.HiringDate);

                _context.SaveChanges();

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
                var technician = _context.Technicians.FirstOrDefault(p => p.TechnicianId == id);

                if (technician is null)
                    return NotFound();

                _context.Technicians.Remove(technician);
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
