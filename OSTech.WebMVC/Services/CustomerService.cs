using OSTech.WebMVC.Models;
using System.Text;
using System.Text.Json;

namespace OSTech.WebMVC.Services
{
    public class CustomerService : ICustomerService
    {
        private string apiEndpoint = "api/v1/Customer";
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        private CustomerViewModel customerVM;
        private IEnumerable<CustomerViewModel> customersVM;

        public CustomerService(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _clientFactory = clientFactory;
        }

   
        public async Task<IEnumerable<CustomerViewModel>> GetCustomersAsync()
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    customersVM = await JsonSerializer
                        .DeserializeAsync<IEnumerable<CustomerViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return customersVM;
        }

        public async Task<CustomerViewModel> GetCustomerByIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("OSTechApi");

            using (var response = await client.GetAsync($"{apiEndpoint}/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    customerVM = await JsonSerializer
                                    .DeserializeAsync<CustomerViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return customerVM;
        }
        public async Task<CustomerViewModel> CreateCustomer(CustomerViewModel customerVM)
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            var customer = JsonSerializer.Serialize(customerVM);
            StringContent content = new StringContent(customer, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if(response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    customerVM = await JsonSerializer
                                    .DeserializeAsync<CustomerViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return customerVM;
        }
        public async Task<bool> UpdateCustomer(int id, CustomerViewModel customerVM)
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            using(var response = await client.PutAsJsonAsync($"{apiEndpoint}/{id}", customerVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            using (var response = await client.DeleteAsync($"{apiEndpoint}/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
