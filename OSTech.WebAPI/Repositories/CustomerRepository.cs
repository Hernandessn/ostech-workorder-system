using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.Customer;
using OSTech.WebAPI.Repositories.Generic;

namespace OSTech.WebAPI.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context) { }
        public async Task<Customer?> Update(Customer customer)
        {
            var customerDb = await _context.Customers
                            .FirstOrDefaultAsync(p => p.CustomerId == customer.CustomerId);

            customerDb.SetName(customer.Name);
            customerDb.SetEmail(customer.Email);
            customerDb.SetPhone(customer.Phone);
            customerDb.SetDocument(customer.Document);


            return customer;
        }
    }
}
