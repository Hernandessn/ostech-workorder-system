using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.Equipment;
using OSTech.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using OSTech.Infrastructure.UnitOfWork;

namespace OSTech.WebAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly ILogger<EquipmentController> _logger;
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public EquipmentController(ILogger<EquipmentController> logger, IUnitOfWork uof, IMapper mapper)
        {
            _logger = logger;
            _uof = uof;
            _mapper = mapper;
        }
        /// <summary>
        /// Obtém todos os equipamentos cadastrados 
        /// </summary>
        /// <returns>Lista de Equipamentos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<EquipmentDTO>>> Get()
        {
            try
            {
                var equipments = await _uof.EquipmentRepository.GetAll();
                var equipmentsDto = _mapper.Map<IEnumerable<EquipmentDTO>>(equipments);

                return Ok(equipmentsDto);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        /// <summary>
        /// Obter um equipamento pelo Id
        /// </summary>
        /// <param name="id">Id do equipamento</param>
        /// <returns>Equipamento encontrado</returns>
        [HttpGet("{id:int:min(1)}", Name = "GetEquipments")]
        public async Task<ActionResult<EquipmentDTO>> Get(int id)
        {
            var equipment = await _uof.EquipmentRepository.GetById(c => c.EquipmentId == id);

            if (equipment is null)
            {
                _logger.LogWarning($"Equipment with id = {id} not found...");
                return NotFound("Equipment not found.");
            }

            var dto = _mapper.Map<EquipmentDTO>(equipment);

            return Ok(dto);

        }
        [HttpPost]
        public async Task<ActionResult<EquipmentDTO>> Post(CreateEquipmentDTO dto)
        {
            if(dto is null)
                return BadRequest();

            var equipment = new Equipment(
                dto.Name,
                dto.Brand,
                dto.Model,
                dto.SerialNumber
            );

            await _uof.EquipmentRepository.Create(equipment);
            await _uof.CommitAsync();

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<EquipmentDTO>> Put(int id, UpdateEquipmentDTO dto)
        {
            if (dto is null)
                return BadRequest();

            if (id <= 0)
                return BadRequest();

            var equipment = await _uof.EquipmentRepository.GetById(c => c.EquipmentId == id);

            if (equipment is null)
            {
                _logger.LogWarning($"Equipment with id = {id} not found...");
                return NotFound("Equipment not found.");
            }

            equipment.SetName(dto.Name);
            equipment.SetBrand(dto.Brand);
            equipment.SetModel(dto.Model);
            equipment.SetSerialNumber(dto.SerialNumber);

            await _uof.EquipmentRepository.Update(equipment);
            await _uof.CommitAsync();

            await _uof.EquipmentRepository.Update(equipment);
            await _uof.CommitAsync();

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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var equipment = await _uof.EquipmentRepository.GetById(c => c.EquipmentId == id);

            if (equipment is null)
            {
                _logger.LogWarning($"Equipment with id = {id} not found...");
                return NotFound("Equipment not found.");
            }

            await _uof.EquipmentRepository.Delete(equipment.EquipmentId);
            await _uof.CommitAsync();

            return NoContent();

        }
    }
}
