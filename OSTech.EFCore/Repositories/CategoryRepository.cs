using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Repositories.Generic;

namespace OSTech.WebAPI.Repositories
{
    public class CategoryRepository : Repository<Category>,  ICategoryRepository
    {

        public CategoryRepository(AppDbContext context) : base(context) { }
        public async Task<Category?> Update(Category category)
        {
            var categoryDb = await _context.Categories
                               .FirstOrDefaultAsync(p => p.CategoryId == category.CategoryId);

            if (category is null)
                return null;

            categoryDb.SetName(category.Name);
            categoryDb.SetDescription(category.Description);


            return categoryDb;
        }
    }
}
