using OSTech.Domain.Entities;
using OSTech.WebAPI.Repositories.Generic;

namespace OSTech.WebAPI.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> Update(Customer customer);
    }
}
