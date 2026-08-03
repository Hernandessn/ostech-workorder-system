using OSTech.WebMVC.Models;

namespace OSTech.WebMVC.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerViewModel>> GetCustomersAsync();
        Task<CustomerViewModel> GetCustomerByIdAsync(int id);
        Task<CustomerViewModel> CreateCustomer(CustomerViewModel customerVM);
        Task<bool> UpdateCustomer(int id, CustomerViewModel customerVM);
        Task<bool> DeleteCustomer(int id);
    }
}
