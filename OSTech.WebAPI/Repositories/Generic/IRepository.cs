using System.Linq.Expressions;

namespace OSTech.WebAPI.Repositories.Generic
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(Expression<Func<T, bool>> predicate);
        Task<T> Create(T entity);
        Task<T> Delete(int id);
    }
}
