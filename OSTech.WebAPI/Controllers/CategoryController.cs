using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.WebAPI.Dtos.Category;
using OSTech.WebAPI.Repositories;
using OSTech.WebAPI.Repositories.UnitOfWork;

namespace OSTech.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public CategoryController(ILogger<CategoryController> logger, IUnitOfWork uof, IMapper mapper)
        {
            _logger = logger;
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _uof.CategoryRepository.GetAll();

            var categoritesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

            return Ok(categoritesDto);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _uof.CategoryRepository.GetById(c => c.CategoryId == id);

            if (category is null)
            {
                _logger.LogWarning($"Category with id= {id} not found...");
                return NotFound("Category not found.");
            }

            var dto = _mapper.Map<CategoryDTO>(category);

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

            await _uof.CategoryRepository.Create(category);
            await _uof.CommitAsync();

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
            var category = await _uof.CategoryRepository.GetById(c => c.CategoryId == id);

            if (category is null)
            {
                _logger.LogWarning($"Category with id= {id} not found...");
                return NotFound("Category not found.");
            }

            category.SetName(dto.Name);
            category.SetDescription(dto.Description);

            await _uof.CategoryRepository.Update(category);
            await _uof.CommitAsync();

            var categoryDto = new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };

            _logger.LogInformation("Category updated. Id={Id}", id);

            return Ok(categoryDto);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _uof.CategoryRepository.GetById(c => c.CategoryId == id);

            if (category is null)
            {
                _logger.LogWarning($"Category with id= {id} not found...");
                return NotFound("Category not found.");
            }

            await _uof.CategoryRepository.Delete(id);
            await _uof.CommitAsync();

            _logger.LogInformation("Category deleted. Id={Id}", id);

            return NoContent();
        }
    }
}
