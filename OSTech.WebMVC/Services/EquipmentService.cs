using OSTech.WebMVC.Models;
using System.Text;
using System.Text.Json;

namespace OSTech.WebMVC.Services
{
    public class EquipmentService : IEquipmentService
    {
        private const string apiEndpoint = "api/v1/Equipment"; 
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        private EquipmentViewModel equipmentVM;
        private IEnumerable<EquipmentViewModel> equipmentsVM;

        public EquipmentService(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<EquipmentViewModel>> GetEquipmentsAsync()
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    equipmentsVM = await JsonSerializer
                        .DeserializeAsync<IEnumerable<EquipmentViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return equipmentsVM;
        }


        public async Task<EquipmentViewModel> GetEquipmentByIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("OSTechApi");

            using (var response = await client.GetAsync($"{apiEndpoint}/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    equipmentVM = await JsonSerializer
                                    .DeserializeAsync<EquipmentViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return equipmentVM;
        }



        public async Task<EquipmentViewModel> CreateEquipment(EquipmentViewModel equipmentVM)
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            var category = JsonSerializer.Serialize(equipmentVM);
            StringContent content = new StringContent(category, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    equipmentVM = await JsonSerializer
                                    .DeserializeAsync<EquipmentViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return equipmentVM;
        }
        public async Task<bool> UpdateEquipmentAsync(int id, EquipmentViewModel equipmentVM)
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            using (var response = await client.PutAsJsonAsync($"{apiEndpoint}/{id}", equipmentVM))
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

        public async Task<bool> DeleteEquipment(int id)
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
