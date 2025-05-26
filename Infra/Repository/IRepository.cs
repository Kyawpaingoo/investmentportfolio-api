using System.Linq.Expressions;

namespace Infra.Repository;

public interface IRepository<T>
{
    int MaxNumber(Expression<Func<T, int>> expression);
    int Count(Expression<Func<T, bool>> filter, Expression<Func<T, int>> field);
    IQueryable<T> GetAll();
    IQueryable<T> GetWithoutTracking();
    int Insert(T entity);
    T InsertReturn(T entity);
    Task<T> InsertReturnAsync(T entity);
    Task InsertListAsync(List<T> entity);
    Task updateListAsync(List<T> entities);
    Task<T> UpdateAsync(T Entity);
    T UpdateComplete(T OldEntity, T NewEntity);
    Task<T> UpdateCompleteAsync(T OldEntity, T NewEntity);
    Task<int> RemoveAsync(T entity);
    int Remove(T entity);
    T GetById(int id);
    T GetByCompositeKey(int id, string key);
    IQueryable<T> Query(Expression<Func<T, bool>> filter, bool showDeleted = false);
    IQueryable<T> Take(int count);
    IQueryable<T> Skip(int count);
    IQueryable<T> OrderBy(Expression<Func<T, string>> filter);
    Task<T> UpdateNewAsync(T Entity);
    void Dispose(bool disposing);
}