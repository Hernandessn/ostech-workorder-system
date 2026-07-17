using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.Customer;
using OSTech.WebAPI.Dtos.WorkOrder;

namespace OSTech.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;
        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> Get()
        {
            try
            {
                var customers = _context.Customers
                                         .AsNoTracking()
                                         .Select(t => new CustomerDTO
                                         {
                                             CustomerId = t.CustomerId,
                                             Name = t.Name,
                                             Email = t.Email,
                                             Phone = t.Phone,
                                             Document = t.Document
                                         })
                                         .ToList();
                return Ok(customers);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                           "An issue occurred while processing your request.");
            }
        }

        [HttpGet("{id:int}", Name = "GetCustomers")]
        public ActionResult<WorkOrderDTO> Get(int id)
        {
            try
            {
                var customer = _context.Customers
                                         .AsNoTracking()
                                         .FirstOrDefault(t => t.CustomerId == id);

                if (customer is null)
                    return NotFound();

                var dto = new CustomerDTO
                {
                    CustomerId = customer.CustomerId,
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Document = customer.Document
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
        public ActionResult<CustomerDTO> Post(CreateCustomerDTO dto)
        {
            try
            {
                var customer = new Customer(
                    dto.Name,
                    dto.Email,
                    dto.Phone,
                    dto.Document
                );

                _context.Customers.Add(customer);
                _context.SaveChanges();

                var customerDTO = new CustomerDTO
                {
                    CustomerId = customer.CustomerId,
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Document = customer.Document
                };

                return CreatedAtRoute(
                    "GetWorkOrder",
                    new { id = customer.CustomerId },
                    customerDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            "An issue occurred while processing your request.");
            }
        }
        [HttpPut("{id:int}")]
        public ActionResult<CustomerDTO> Put(int id, UpdateCustomerDTO dto)
        {
            try
            {
                var customer = _context.Customers.Find(id);

                if (customer is null)
                    return NotFound();


                customer.SetName(dto.Name);
                customer.SetEmail(dto.Email);
                customer.SetPhone(dto.Phone);
                customer.SetDocument(dto.Document);

                _context.SaveChanges();

                var customerDTO = new CustomerDTO
                {
                    CustomerId = customer.CustomerId,
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Document = customer.Document
                };

                return Ok(customerDTO);
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
                var customer = _context.Customers.FirstOrDefault(p => p.CustomerId == id);

                if (customer is null)
                    return NotFound();

                _context.Customers.Remove(customer);
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
