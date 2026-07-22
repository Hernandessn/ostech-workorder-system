using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.Customer;
using OSTech.WebAPI.Dtos.WorkOrder;
using OSTech.WebAPI.Repositories;
using OSTech.WebAPI.Repositories.UnitOfWork;

namespace OSTech.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public CustomerController(ILogger<CustomerController> logger, IUnitOfWork uof, IMapper mapper)
        {
            _logger = logger;
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> Get()
        {
            var customers = await _uof.CustomerRepository.GetAll();

            var customersDto = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

            return Ok(customersDto);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetCustomers")]
        public async Task<ActionResult<CustomerDTO>> Get(int id)
        {
            var customer = await _uof.CustomerRepository.GetById(c => c.CustomerId == id);

            if (customer is null)
            {
                _logger.LogWarning($"Customer with id= {id} not found...");
                return NotFound("Customer not found.");
            }

            var dto = _mapper.Map<CustomerDTO>(customer);

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

            await _uof.CustomerRepository.Create(customer);
            await _uof.CommitAsync();

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

            var customer = await _uof.CustomerRepository.GetById(c => c.CustomerId == id);

            if (customer is null)
            {
                _logger.LogWarning($"Customer with id= {id} not found...");
                return NotFound("Customer not found.");
            }


            await _uof.CustomerRepository.Update(customer);
            await _uof.CommitAsync();

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
            var customer = await _uof.CustomerRepository.GetById(c => c.CustomerId == id);

            if (customer is null)
            {
                _logger.LogWarning($"Customer with id= {id} not found...");
                return NotFound("Customer not found.");
            }

            await _uof.CustomerRepository.Delete(customer.CustomerId);
            await _uof.CommitAsync();

            return NoContent();

        }
    }
}
