using OSTech.WebMVC.Models;
using System.Text;
using System.Text.Json;

namespace OSTech.WebMVC.Services
{
    public class CategoryService : ICategoryService
    {
        private const string apiEndpoint = "api/v1/Category";
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        private CategoryViewModel categoryVM;
        private IEnumerable<CategoryViewModel> categoriesVM;

        public CategoryService(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categoriesVM = await JsonSerializer
                        .DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return categoriesVM;
        }

        public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("OSTechApi");

            using (var response = await client.GetAsync($"{apiEndpoint}/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categoryVM = await JsonSerializer
                                    .DeserializeAsync<CategoryViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return categoryVM;
        }
        public async Task<CategoryViewModel> CreateCategory(CategoryViewModel categoryVM)
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            var category = JsonSerializer.Serialize(categoryVM);
            StringContent content = new StringContent(category, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categoryVM = await JsonSerializer
                                    .DeserializeAsync<CategoryViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return categoryVM;
        }
        public async Task<bool> UpdateCategoryAsync(int id, CategoryViewModel categoryVM)
        {
            var client = _clientFactory.CreateClient("OSTechApi");
            using (var response = await client.PutAsJsonAsync($"{apiEndpoint}/{id}", categoryVM))
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

        public async Task<bool> DeleteCategory(int id)
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

