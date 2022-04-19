using Common.Domain.BaseClasses;

namespace Common.Domain.Repository;

public interface IBaseRepository<T> where T : BaseAggregateRoot
{
    T? Get(long id);
    Task<T?> GetAsync(long id);
    Task<T?> GetAsTrackingAsync(long id);
    void Add(T entity);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    bool Exists(Func<T, bool> predicate);
    Task<bool> ExistsAsync(Func<T, bool> predicate);
    Task SaveAsync();
}