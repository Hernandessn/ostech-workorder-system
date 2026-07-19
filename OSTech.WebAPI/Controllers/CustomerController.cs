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
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(AppDbContext context, ILogger<CustomerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> Get()
        {
            var customers = await _context.Customers
                                     .AsNoTracking()
                                     .Select(t => new CustomerDTO
                                     {
                                         CustomerId = t.CustomerId,
                                         Name = t.Name,
                                         Email = t.Email,
                                         Phone = t.Phone,
                                         Document = t.Document
                                     })
                                     .ToListAsync();
            return Ok(customers);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetCustomers")]
        public async Task<ActionResult<CustomerDTO>> Get(int id)
        {
            var customer = await _context.Customers
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(t => t.CustomerId == id);

            if (customer is null)
            {
                _logger.LogWarning($"Customer with id= {id} not found...");
                return NotFound("Customer not found.");
            }

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
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> Post(CreateCustomerDTO dto)
        {
            var customer = new Customer(
                dto.Name,
                dto.Email,
                dto.Phone,
                dto.Document
            );

            if (customer is null)
            {
                _logger.LogWarning($"Invalid data...");
                return BadRequest("Invalid data");
            }

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            var customerDTO = new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Document = customer.Document
            };

            return CreatedAtRoute(
                "GetCustomers",
                new { id = customer.CustomerId },
                customerDTO);
        }
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<CustomerDTO>> Put(int id, UpdateCustomerDTO dto)
        {

            var customer = await _context.Customers.FindAsync(id);

            if (customer is null)
            {
                _logger.LogWarning($"Customer with id= {id} not found...");
                return NotFound("Customer not found.");
            }


            customer.SetName(dto.Name);
            customer.SetEmail(dto.Email);
            customer.SetPhone(dto.Phone);
            customer.SetDocument(dto.Document);

            await _context.SaveChangesAsync();

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

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer is null)
            {
                _logger.LogWarning($"Customer with id= {id} not found...");
                return NotFound("Customer not found.");
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
