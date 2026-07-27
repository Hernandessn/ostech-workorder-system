using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.Technician;
using OSTech.WebAPI.Dtos.WorkOrder;
using OSTech.WebAPI.Repositories;
using OSTech.WebAPI.Repositories.UnitOfWork;

namespace OSTech.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WorkOrderController : ControllerBase
    {
        private readonly ILogger<WorkOrderController> _logger;
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public WorkOrderController(ILogger<WorkOrderController> logger, IUnitOfWork uof, IMapper mapper)
        {
            _logger = logger;
            _uof = uof;
            _mapper = mapper;
        }
        /// <summary>
        /// Obtém uma lista de ordens de serviços cadastrados
        /// </summary>
        /// <returns>Lista de Ordens de Serviços</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkOrderDTO>>> Get()
        {

            var workOrders = await _uof.WorkOrderRepository.GetAll();
            var workOrdersDto = _mapper.Map<IEnumerable<WorkOrderDTO>>(workOrders);

            return Ok(workOrdersDto);

        }
        /// <summary>
        /// Obter uma ordem de serviço pelo Id
        /// </summary>
        /// <param name="id">id da Ordem de Serviço</param>
        /// <returns>Ordem de Serviço encontrada</returns>
        [HttpGet("{id:int:min(1)}", Name = "GetWorkOrder")]
        public async Task<ActionResult<WorkOrderDTO>> Get(int id)
        {

            var workOrder = await _uof.WorkOrderRepository.GetById(c => c.WorkOrderId == id);

            if (workOrder is null)
            {
                _logger.LogWarning($"WorkOrder with id= {id} not found...");
                return NotFound("WorkOrder not found.");
            }

            var dto = _mapper.Map<WorkOrderDTO>(workOrder);

            return Ok(dto);

        }
        [HttpPost]
        public async Task<ActionResult<WorkOrderDTO>> Post(CreateWorkOrderDTO dto)
        {

            var workOrder = new WorkOrder(
                dto.Description,
                dto.Title,
                dto.Amount,
                dto.Deadline,
                dto.OpeningDate,
                dto.TechnicianId,
                dto.CustomerId,
                dto.CategoryId,
                dto.EquipmentId
            );

            await _uof.WorkOrderRepository.Create(workOrder);
            await _uof.CommitAsync();

            var workOrderDTO = new WorkOrderDTO
            {
                WorkOrderId = workOrder.WorkOrderId,
                TechnicianId = workOrder.TechnicianId,
                Title = workOrder.Title,
                Description = workOrder.Description,
                Amount = workOrder.Amount,
                Deadline = workOrder.Deadline,
                OpeningDate = workOrder.OpeningDate,
                CustomerId = workOrder.CustomerId,
                CategoryId = workOrder.CategoryId,
                EquipmentId = workOrder.EquipmentId
            };

            return CreatedAtRoute(
                "GetWorkOrder",
                new { id = workOrder.WorkOrderId },
                workOrderDTO);

        }
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<WorkOrderDTO>> Put(int id, UpdateWorkOrderDTO dto)
        {

            var workOrder = await _uof.WorkOrderRepository.GetById(c => c.WorkOrderId == id);

            if (workOrder is null)
            {
                _logger.LogWarning($"WorkOrder with id= {id} not found...");
                return NotFound("WorkOrder not found.");
            }

            workOrder.SetDescription(dto.Description);
            workOrder.SetTitle(dto.Title);
            workOrder.SetAmount(dto.Amount);
            workOrder.ChangeDeadline(dto.Deadline);

            workOrder.AssignTechnician(dto.TechnicianId);
            workOrder.AssignCustomer(dto.CustomerId);
            workOrder.AssignCategory(dto.CategoryId);
            workOrder.AssignEquipment(dto.EquipmentId);

            await _uof.WorkOrderRepository.Update(workOrder);
            await _uof.CommitAsync();

            var workOrderDto = new WorkOrderDTO
            {
                WorkOrderId = workOrder.WorkOrderId,
                Description = workOrder.Description,
                Title = workOrder.Title,
                Amount = workOrder.Amount,
                Deadline = workOrder.Deadline,
                OpeningDate = workOrder.OpeningDate,
                TechnicianId = workOrder.TechnicianId,
                CustomerId = workOrder.CustomerId,
                CategoryId = workOrder.CategoryId,
                EquipmentId = workOrder.EquipmentId
            };

            return Ok(workOrderDto);

        }
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {

            var workOrder = await _uof.WorkOrderRepository.GetById(c => c.WorkOrderId == id);

            if (workOrder is null)
            {
                _logger.LogWarning($"WorkOrder with id= {id} not found...");
                return NotFound("WorkOrder not found.");
            }

            await _uof.WorkOrderRepository.Delete(id);
            await _uof.CommitAsync();

            return NoContent();

        }
    }
}
