using OSTech.WebMVC.Models;
using System.Text;
using System.Text.Json;

namespace OSTech.WebMVC.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private const string apiEndpoint = "api/v1/WorkOrder";
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        private WorkOrderViewModel workOrderVM;
        private IEnumerable<WorkOrderViewModel> workOrdersVM;

        public WorkOrderService(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<WorkOrderViewModel>> GetWorkOrdersAsync()
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    workOrdersVM = await JsonSerializer
                        .DeserializeAsync<IEnumerable<WorkOrderViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return workOrdersVM;
        }
        public async Task<WorkOrderViewModel> GetWorkOrderByIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("OSTechApi");

            using (var response = await client.GetAsync($"{apiEndpoint}/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    workOrderVM = await JsonSerializer
                                    .DeserializeAsync<WorkOrderViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return workOrderVM;
        }
        public async Task<WorkOrderViewModel> CreateWorkOrder(WorkOrderViewModel workOrderVM)
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            var category = JsonSerializer.Serialize(workOrderVM);
            StringContent content = new StringContent(category, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    workOrderVM = await JsonSerializer
                                    .DeserializeAsync<WorkOrderViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return workOrderVM;
        }

        public async Task<bool> UpdateWorkOrderAsync(int id, WorkOrderViewModel workOrderVM)
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            using (var response = await client.PutAsJsonAsync($"{apiEndpoint}/{id}", workOrderVM))
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
        public async Task<bool> DeleteWorkOrder(int id)
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
