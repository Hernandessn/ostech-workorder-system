using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Dtos.Category;

namespace OSTech.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(AppDbContext context, ILogger<CategoryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _context.Categories
                                     .AsNoTracking()
                                     .Select(t => new CategoryDTO
                                     {
                                         CategoryId = t.CategoryId,
                                         Name = t.Name,
                                         Description = t.Description
                                     })
                                     .ToListAsync();
            return Ok(categories);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _context.Categories
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(t => t.CategoryId == id);

            if (category is null)
            {
                _logger.LogWarning($"Category with id= {id} not found...");
                return NotFound("Category not found.");
            }


            var dto = new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };

            return Ok(dto);
        }
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post(CreateCategoryDTO dto)
        {
            var category = new Category(
                dto.Name,
                dto.Description
            );
            if (category is null)
            {
                _logger.LogWarning($"Invalid data...");
                return BadRequest("Invalid data");
            }
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            var categoryDTO = new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };

            return CreatedAtRoute(
                "GetCategory",
                new { id = category.CategoryId },
                categoryDTO);
        }
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<CategoryDTO>> Put(int id, UpdateCategoryDTO dto)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null)
            {
                _logger.LogWarning($"Category with id= {id} not found...");
                return NotFound("Category not found.");
            }

            category.SetName(dto.Name);
            category.SetDescription(dto.Description);

            await _context.SaveChangesAsync();

            var categoryDTO = new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };

            return Ok(categoryDTO);
        }
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null)
            {
                _logger.LogWarning($"Category with id= {id} not found...");
                return NotFound("Category not found.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
