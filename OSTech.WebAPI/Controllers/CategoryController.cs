using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.WebAPI.Dtos.Category;
using OSTech.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using OSTech.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Caching.Memory;

namespace OSTech.WebAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [EnableRateLimiting("fixedwindow")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private const string CacheCategoriesKey = "CacheCategories";
        public CategoryController(ILogger<CategoryController> logger, IUnitOfWork uof, IMapper mapper, IMemoryCache cache)
        {
            _logger = logger;
            _uof = uof;
            _mapper = mapper;
            _cache = cache;
        }
        /// <summary>
        /// Obtém todas as categorias cadastradas
        /// </summary>
        /// <returns>Lista de Categorias</returns>
        //[Authorize(Policy = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            try
            {
                if (!_cache.TryGetValue(CacheCategoriesKey, out IEnumerable<Category>? categories))
                {

                    categories = await _uof.CategoryRepository.GetAll();
                    if (categories is null || !categories.Any())
                    {
                        _logger.LogWarning("Não existem categorias...");
                        return NotFound("Não existem categorias...");
                    }
                    SetCache(CacheCategoriesKey, categories);
                }

                var categoritesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

                return Ok(categoritesDto);
            }
            catch
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Obter uma categoria pelo Id
        /// </summary>
        /// <param name="id">Id da categoria</param>
        /// <returns>A categoria encontrada</returns>
       // [Authorize(Policy = "User")]
        [HttpGet("{id:int:min(1)}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var cacheKey = GetCacheCategoryKey(id);
            if (!_cache.TryGetValue(cacheKey, out Category? category))
            {
                category = await _uof.CategoryRepository.GetById(c => c.CategoryId == id);

                if (category is null)
                {
                    _logger.LogWarning("Category with id {Id} not found.", id);
                    return NotFound("Category not found.");
                }
                SetCache(cacheKey, category);
            }

            var dto = _mapper.Map<CategoryDTO>(category);

            return Ok(dto);
        }
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post(CreateCategoryDTO dto)
        {
            if (dto is null)
                return BadRequest();

            var category = new Category(
                dto.Name,
                dto.Description
            );

            await _uof.CategoryRepository.Create(category);
            await _uof.CommitAsync();

            InvalidateCacheAfterChange(category.CategoryId, category);

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CategoryDTO>> Put(int id, UpdateCategoryDTO dto)
        {
            if (dto is null)
                return BadRequest();

            if (id <= 0)
                return BadRequest();


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

            InvalidateCacheAfterChange(id, category);

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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
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

            InvalidateCacheAfterChange(id);

            _logger.LogInformation("Category deleted. Id={Id}", id);

            return NoContent();
        }
        private string GetCacheCategoryKey(int id) => $"CacheCategory_{id}";
        private void SetCache<T>(string key, T data)
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(15),
                Priority = CacheItemPriority.High
            };
            _cache.Set(key, data, cacheOptions);
        }
        private void InvalidateCacheAfterChange(int id, Category? category = null)
        {
            _cache.Remove($"CacheCategory_{id}");
            _cache.Remove(CacheCategoriesKey);

            if (category != null)
                SetCache(GetCacheCategoryKey(id), category);
        }
    }
}
