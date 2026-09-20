using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OSTech.Domain.Exceptions;
using OSTech.Infrastructure.UnitOfWork;
using OSTech.WebAPI.Commands.WorkOrders;
using OSTech.WebAPI.Dtos.WorkOrder;
using OSTech.WebAPI.Queries.WorkOrders;

namespace OSTech.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WorkOrderController : ControllerBase
    {
        private readonly ILogger<WorkOrderController> _logger;
        private readonly IUnitOfWork _uof;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public WorkOrderController(ILogger<WorkOrderController> logger, IUnitOfWork uof, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _uof = uof;
            _mapper = mapper;
            _mediator = mediator;
        }
        /// <summary>
        /// Obtém uma lista de ordens de serviços cadastrados
        /// </summary>
        /// <returns>Lista de Ordens de Serviços</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<WorkOrderDTO>>> Get()
        {
            try
            {
                var query = new GetWorkOrdersQuery(); 
                var workOrders = await _mediator.Send(query);

                return Ok(workOrders);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Obter uma ordem de serviço pelo Id
        /// </summary>
        /// <param name="id">id da Ordem de Serviço</param>
        /// <returns>Ordem de Serviço encontrada</returns>
        [HttpGet("{id:int:min(1)}", Name = "GetWorkOrder")]
        public async Task<ActionResult<WorkOrderDTO>> Get(int id)
        {

            var query = new GetWorkOrderByIdQuery { WorkOrderId = id };
            var workOrder = await _mediator.Send(query);

            if (workOrder is null)
            {
                _logger.LogWarning($"WorkOrder with id= {id} not found...");
                return NotFound("WorkOrder not found.");
            }

            return Ok(workOrder);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<WorkOrderDTO>> Post(CreateWorkOrderDTO dto)
        {
            if (dto is null)
                return BadRequest();

            var command = new CreateWorkOrderCommand
            {
                TechnicianId = dto.TechnicianId,
                Title = dto.Title,
                Description = dto.Description,
                Amount = dto.Amount,
                Deadline = dto.Deadline,
                OpeningDate = dto.OpeningDate,
                CustomerId = dto.CustomerId,
                CategoryId = dto.CategoryId,
                EquipmentId = dto.EquipmentId
            };

            var workOrder = await _mediator.Send(command);

            var workOrderDTO = new WorkOrderDTO
            {
                WorkOrderId = workOrder.WorkOrderId,
                Description = workOrder.Description,
                Title = workOrder.Title,
                Status = workOrder.Status,
                Amount = workOrder.Amount,
                Deadline = workOrder.Deadline,
                OpeningDate = workOrder.OpeningDate,
                TechnicianId = workOrder.TechnicianId,
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
            if (dto is null)
                return BadRequest();

            if (id <= 0)
                return BadRequest();

            var command = new UpdateWorkOrderCommand
            {
                WorkOrderId = id,
                Description = dto.Description,
                Title = dto.Title,
                Amount = dto.Amount,
                Deadline = dto.Deadline,
                TechnicianId = dto.TechnicianId,
                CustomerId = dto.CustomerId,
                CategoryId = dto.CategoryId,
                EquipmentId = dto.EquipmentId
            };

            var workOrder = await _mediator.Send(command);

            var workOrderDto = new WorkOrderDTO
            {
                WorkOrderId = workOrder.WorkOrderId,
                Description = workOrder.Description,
                Title = workOrder.Title,
                Status = workOrder.Status,
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

        [HttpPatch("{id:int:min(1)}/start")]
        public async Task<ActionResult> Start(int id)
        {
            var command = new StartWorkOrderCommand { WorkOrderId = id };

            var workOrder = await _mediator.Send(command);

            var workOrderDto = new WorkOrderDTO
            {
                WorkOrderId = workOrder.WorkOrderId,
                Description = workOrder.Description,
                Title = workOrder.Title,
                Status = workOrder.Status,
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
        [HttpPatch("{id:int:min(1)}/complete")]
        public async Task<ActionResult> Complete(int id)
        {
            var command = new CompleteWorkOrderCommand { WorkOrderId = id };
            var workOrder = await _mediator.Send(command);

            var workOrderDto = new WorkOrderDTO
            {
                WorkOrderId = workOrder.WorkOrderId,
                Description = workOrder.Description,
                Title = workOrder.Title,
                Status = workOrder.Status,
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

        [HttpPatch("{id:int:min(1)}/cancel")]
        public async Task<ActionResult> Cancel(int id)
        {
            var command = new CancelWorkOrderCommand { WorkOrderId = id };

            var workOrder = await _mediator.Send(command);

            var workOrderDto = new WorkOrderDTO
            {
                WorkOrderId = workOrder.WorkOrderId,
                Description = workOrder.Description,
                Title = workOrder.Title,
                Status = workOrder.Status,
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteWorkOrderCommand { WorkOrderId = id };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (DomainException)
            {
                _logger.LogWarning($"WorkOrder with id= {id} not found...");
                return NotFound("WorkOrder not found.");
            }
        }
    }
}
