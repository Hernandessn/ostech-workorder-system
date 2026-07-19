using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.Technician;
using OSTech.WebAPI.Dtos.WorkOrder;

namespace OSTech.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkOrderController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<WorkOrderController> _logger;


        public WorkOrderController(AppDbContext context, ILogger<WorkOrderController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkOrderDTO>>> Get()
        {

            var workOrders = await _context.WorkOrders
                                     .AsNoTracking()
                                     .Select(t => new WorkOrderDTO
                                     {
                                         WorkOrderId = t.WorkOrderId,
                                         TechnicianId = t.TechnicianId,
                                         Title = t.Title,
                                         Description = t.Description,
                                         Amount = t.Amount,
                                         Deadline = t.Deadline,
                                         OpeningDate = t.OpeningDate,
                                         CustomerId = t.CustomerId,
                                         CategoryId = t.CategoryId,
                                         EquipmentId = t.EquipmentId
                                     })
                                     .ToListAsync();
            return Ok(workOrders);

        }

        [HttpGet("{id:int:min(1)}", Name = "GetWorkOrder")]
        public async Task<ActionResult<WorkOrderDTO>> Get(int id)
        {

            var workOrder = await _context.WorkOrders
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(t => t.WorkOrderId == id);

            if (workOrder is null)
            {
                _logger.LogWarning($"WorkOrder with id= {id} not found...");
                return NotFound("WorkOrder not found.");
            }

            var dto = new WorkOrderDTO
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

            await _context.WorkOrders.AddAsync(workOrder);
            await _context.SaveChangesAsync();

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

            var workOrder = await _context.WorkOrders.FindAsync(id);

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

            await _context.SaveChangesAsync();

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

            var workOrder = await _context.WorkOrders.FindAsync(id);

            if (workOrder is null)
            {
                _logger.LogWarning($"WorkOrder with id= {id} not found...");
                return NotFound("WorkOrder not found.");
            }

            _context.WorkOrders.Remove(workOrder);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
