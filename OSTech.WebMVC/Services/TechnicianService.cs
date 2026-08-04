using OSTech.WebMVC.Models;
using System.Text;
using System.Text.Json;

namespace OSTech.WebMVC.Services
{
    public class TechnicianService : ITechnicianService
    {
        private const string apiEndpoint = "api/v1/Technician";
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        private TechnicianViewModel technicianVM;
        private IEnumerable<TechnicianViewModel> techniciansVM;

        public TechnicianService(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<TechnicianViewModel>> GetTechniciansAsync()
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    techniciansVM = await JsonSerializer
                                        .DeserializeAsync<IEnumerable<TechnicianViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return techniciansVM;
        }
        public async Task<TechnicianViewModel> GetTechnicianByIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("OSTechApi");

            using (var response = await client.GetAsync($"{apiEndpoint}/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    technicianVM = await JsonSerializer
                                    .DeserializeAsync<TechnicianViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return technicianVM;
        }

        public async Task<TechnicianViewModel> CreateTechnician(TechnicianViewModel technicianVM)
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            var technician = JsonSerializer.Serialize(technicianVM);
            StringContent content = new StringContent(technician, Encoding.UTF8, "application/json");

            using(var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    technicianVM = await JsonSerializer
                                    .DeserializeAsync<TechnicianViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return technicianVM;
        }

        public async Task<bool> UpdateTechnicianAsync(int id, TechnicianViewModel technicianVM)
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            using (var response = await client.PutAsJsonAsync($"{apiEndpoint}/{id}", technicianVM))
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

        public async Task<bool> DeleteTechnician(int id)
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
