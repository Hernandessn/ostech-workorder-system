using Microsoft.EntityFrameworkCore;
using OSTech.EFCore.Context;
using System.Linq.Expressions;

namespace OSTech.WebAPI.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetById(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T?> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            return entity;
        }

        public async Task<T?> Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity is null)
                return null;

            _context.Set<T>().Remove(entity);

            return entity;
        }
    }
}
