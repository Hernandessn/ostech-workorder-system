using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.Category;
using OSTech.WebAPI.Dtos.WorkOrder;

namespace OSTech.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryController>> Get()
        {
            try
            {
                var categories = _context.Categories
                                         .AsNoTracking()
                                         .Select(t => new CategoryDTO
                                         {
                                             CategoryId = t.CategoryId,
                                             Name = t.Name,
                                             Description = t.Description
                                         })
                                         .ToList();
                return Ok(categories);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                           "An issue occurred while processing your request.");
            }
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<CategoryDTO> Get(int id)
        {
            try
            {
                var category = _context.Categories
                                         .AsNoTracking()
                                         .FirstOrDefault(t => t.CategoryId == id);

                if (category is null)
                    return NotFound();

                var dto = new CategoryDTO
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Description = category.Description
                };

                return Ok(dto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            "An issue occurred while processing your request.");
            }
        }
        [HttpPost]
        public ActionResult<CategoryDTO> Post(CreateCategoryDTO dto)
        {
            try
            {
                var category = new Category(
                    dto.Name,
                    dto.Description
                );

                _context.Categories.Add(category);
                _context.SaveChanges();

                var categoryDTO = new CategoryDTO
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Description = category.Description
                };

                return CreatedAtRoute(
                    "GetWorkOrder",
                    new { id = category.CategoryId },
                    categoryDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            "An issue occurred while processing your request.");
            }
        }
        [HttpPut("{id:int}")]
        public ActionResult<CategoryDTO> Put(int id, UpdateCategoryDTO dto)
        {
            try
            {
                var category = _context.Categories.Find(id);

                if (category is null)
                    return NotFound();

                category.SetName(dto.Name);
                category.SetDescription(dto.Description);

                _context.SaveChanges();

                var categoryDTO = new CategoryDTO
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Description = category.Description
                };

                return Ok(categoryDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            "An issue occurred while processing your request.");
            }
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);

                if (category is null)
                    return NotFound();

                _context.Categories.Remove(category);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            "An issue occurred while processing your request.");
            }
        }
    }
}
