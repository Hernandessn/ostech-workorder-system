using OSTech.Domain.Entities;
using OSTech.WebAPI.Repositories.Generic;

namespace OSTech.WebAPI.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> Update(Category category);
    }
}
