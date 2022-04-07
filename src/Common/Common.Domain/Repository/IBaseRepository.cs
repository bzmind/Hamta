using Common.Domain.Base_Classes;

namespace Common.Domain.Repository;

public interface IBaseRepository<T> where T : BaseAggregateRoot
{
    Task<T?> GetAsync(long id);
    Task<T?> GetAsTrackingAsync(long id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    bool Exists(Func<T, bool> predicate);
    Task<bool> ExistsAsync(Func<T, bool> predicate);
    Task SaveAsync();
}