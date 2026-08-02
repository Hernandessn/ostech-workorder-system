using Microsoft.AspNetCore.Mvc;
using OSTech.WebMVC.Models;
using OSTech.WebMVC.Services;

namespace OSTech.WebMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> Index()
        {
            var result = await _categoryService.GetCategoriesAsync();

            if (result is null)
                return View("Error");

            return View(result);
        }
        [HttpGet]
        public IActionResult CreateNewCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<CategoryViewModel>> CreateNewCategory(CategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.CreateCategory(categoryVM);

                if(result != null)
                    return RedirectToAction(nameof(Index));
            }
            ViewBag.Erro = "Error creating category";
            return View(categoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryViewModel>> UpdateCategory(int id,  CategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.UpdateCategoryAsync(id, categoryVM);

                if(result)
                    return RedirectToAction(nameof(Index));
            }
            ViewBag.Erro = "Error updating category";
            return View(categoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);

            if (result == null)
                return View("Error");

            return View(result);
        }

        [HttpPost, ActionName("DeleteCategory")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            if (result)
                return RedirectToAction("Index");

            return View(result);
        }


    }
}
