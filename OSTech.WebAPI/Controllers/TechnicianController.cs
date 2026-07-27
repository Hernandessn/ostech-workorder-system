using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OSTech.Domain.Entities;
using OSTech.WebAPI.Dtos.Technician;
using OSTech.WebAPI.Filters;
using OSTech.WebAPI.Pagination;
using OSTech.WebAPI.Repositories.UnitOfWork;
using System.Text.Json.Serialization;

namespace OSTech.WebAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TechnicianController : ControllerBase
    {
        private readonly ILogger<TechnicianController> _logger;
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public TechnicianController(ILogger<TechnicianController> logger, IUnitOfWork uof, IMapper mapper)
        {
            _logger = logger;
            _uof = uof;
            _mapper = mapper;
        }
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<TechnicianDTO>>> GetFilter([FromQuery]
                                                                    TechnicianFilterParameters parameters)
        {
            var technicians = await _uof.TechnicianRepository.Filter(parameters);

            var techniciansDto = _mapper.Map<IEnumerable<TechnicianDTO>>(technicians);

            return Ok(techniciansDto);
        }
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<TechnicianDTO>>> Get([FromQuery] 
                                                                        TechnicianParameters technicianParams)
        {
            var technicians = await _uof.TechnicianRepository.GetTechnicians(technicianParams);
            var metadata = new
            {
                technicians.TotalCount,
                technicians.PageSize,
                technicians.CurrentPage,
                technicians.TotalPages,
                technicians.HasNext,
                technicians.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var techniciansDto = _mapper.Map<IEnumerable<TechnicianDTO>>(technicians);

            return Ok(techniciansDto);
        }
        /// <summary>
        /// Obtém todos os técnicos cadastrados.
        /// </summary>
        /// <returns>Lista de técnicos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TechnicianDTO>>> Get()
        {
            var technicians = await _uof.TechnicianRepository.GetAll();

            var techniciansDto = _mapper.Map<IEnumerable<TechnicianDTO>>(technicians);

            return Ok(techniciansDto);
        }
        /// <summary>
        /// Obter um técnico pelo Id.
        /// </summary>
        /// <param name="id">Id do técnico.</param>
        /// <returns>O técnico encontrado.</returns>
        [HttpGet("{id:int:min(1)}", Name = "GetTechnician")]
        public async Task<ActionResult<TechnicianDTO>> Get(int id)
        {
            var technician = await _uof.TechnicianRepository.GetById(c => c.TechnicianId == id);

            if (technician is null)
            {
                _logger.LogWarning("Technician with id={Id} not found.", id);
                return NotFound("Technician not found.");
            }

            var dto =_mapper.Map<TechnicianDTO>(technician);

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

            await _uof.TechnicianRepository.Create(technician);
            await _uof.CommitAsync();

            var technicianDto = new TechnicianDTO
            {
                TechnicianId = technician.TechnicianId,
                Name = technician.Name,
                Specialty = technician.Specialty,
                Contact = technician.Contact,
                Availability = technician.Availability,
                HiringDate = technician.HiringDate
            };

            _logger.LogInformation("Technician created. Id={Id}", technician.TechnicianId);

            return CreatedAtRoute(
                "GetTechnician",
                new { id = technician.TechnicianId },
                technicianDto);


        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<TechnicianDTO>> Put(int id, UpdateTechnicianDTO dto)
        {
            var technician = await _uof.TechnicianRepository.GetById(c => c.TechnicianId == id);

            if (technician is null)
            {
                _logger.LogWarning("Technician with id={Id} not found.", id);
                return NotFound("Technician not found.");
            }

            technician.SetName(dto.Name);
            technician.SetSpecialty(dto.Specialty);
            technician.SetContact(dto.Contact);
            technician.SetAvailability(dto.Availability);
            technician.SetHiringDate(dto.HiringDate);

            await _uof.TechnicianRepository.Update(technician);
            await _uof.CommitAsync();

            var technicianDto = new TechnicianDTO
            {
                TechnicianId = technician.TechnicianId,
                Name = technician.Name,
                Specialty = technician.Specialty,
                Contact = technician.Contact,
                Availability = technician.Availability,
                HiringDate = technician.HiringDate
            };

            _logger.LogInformation("Technician updated. Id={Id}", id);

            return Ok(technicianDto);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {

            var technician = await _uof.TechnicianRepository.GetById(c => c.TechnicianId == id);

            if (technician is null)
            {
                _logger.LogWarning("Technician with id={Id} not found.", id);
                return NotFound("Technician not found.");
            }

            await _uof.TechnicianRepository.Delete(id);
            await _uof.CommitAsync();

            _logger.LogInformation("Technician deleted. Id={Id}", id);

            return NoContent();

        }
    }
}
