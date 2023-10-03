using System.Collections;
using System.Linq.Expressions;

namespace RefereeHub.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    T Find(params object[] keyValues);
    Task<T> FindAsync(params object[] keyValues);
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task InsertAsync(T entity);
    Task InsertRangeAsync(IEnumerable entities);
    void InsertRange(IEnumerable entities);
    void Update(T entity);
    void Remove(int id);
    void Remove(T entity);
    Task DeleteRangeAsync(IEnumerable<T> entities);
}