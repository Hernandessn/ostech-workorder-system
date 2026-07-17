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

        public WorkOrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WorkOrderDTO>> Get()
        {
            try
            {
                var workOrders = _context.WorkOrders
                                         .AsNoTracking()
                                         .Select(t => new WorkOrderDTO
                                         {
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
                                         .ToList();
                return Ok(workOrders);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                           "An issue occurred while processing your request.");
            }
        }

        [HttpGet("{id:int}", Name = "GetWorkOrder")]
        public ActionResult<WorkOrderDTO> Get(int id)
        {
            try
            {
                var workOrder = _context.WorkOrders
                                         .AsNoTracking()
                                         .FirstOrDefault(t => t.WorkOrderId == id);

                if (workOrder is null)
                    return NotFound();

                var dto = new WorkOrderDTO
                {
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            "An issue occurred while processing your request.");
            }
        }
        [HttpPost]
        public ActionResult<WorkOrderDTO> Post(CreateWorkOrderDTO dto)
        {
            try
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

                _context.WorkOrders.Add(workOrder);
                _context.SaveChanges();

                var workOrderDTO = new WorkOrderDTO
                {
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            "An issue occurred while processing your request.");
            }
        }
        [HttpPut("{id:int}")]
        public ActionResult<WorkOrderDTO> Put(int id, UpdateWorkOrderDTO dto)
        {
            try
            {
                var workOrder = _context.WorkOrders.Find(id);

                if (workOrder is null)
                    return NotFound();

                workOrder.SetDescription(dto.Description);
                workOrder.SetTitle(dto.Title);
                workOrder.SetAmount(dto.Amount);
                workOrder.ChangeDeadline(dto.Deadline);

                workOrder.AssignTechnician(dto.TechnicianId);
                workOrder.AssignCustomer(dto.CustomerId);
                workOrder.AssignCategory(dto.CategoryId);
                workOrder.AssignEquipment(dto.EquipmentId);

                _context.SaveChanges();

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
                var workOrder = _context.WorkOrders.FirstOrDefault(p => p.WorkOrderId == id);

                if (workOrder is null)
                    return NotFound();

                _context.WorkOrders.Remove(workOrder);
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
