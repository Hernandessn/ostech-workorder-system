using OSTech.WebMVC.Models;

namespace OSTech.WebMVC.Services
{
    public interface ICategoryService
    {
        Task <IEnumerable<CategoryViewModel>> GetCategoriesAsync();
        Task <CategoryViewModel> GetCategoryByIdAsync (int id);
        Task<CategoryViewModel> CreateCategory(CategoryViewModel categoryVM);
        Task<bool> UpdateCategoryAsync (int id, CategoryViewModel categoryVM);
        Task<bool> DeleteCategory(int id);
    }
}
