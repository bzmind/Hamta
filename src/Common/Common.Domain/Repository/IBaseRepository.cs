using Common.Domain.Base_Classes;

namespace Common.Domain.Repository;

public interface IBaseRepository<T> where T : BaseAggregateRoot
{
    T? Get(long id);
    Task<T?> GetAsync(long id);
    T? GetAsTracking(long id);
    Task<T?> GetAsTrackingAsync(long id);
    void Add(T entity);
    Task AddAsync(T entity);
    void Update(T entity);
    Task UpdateAsync(T entity);
    void Delete(T entity);
    Task DeleteAsync(T entity);
    bool Exists(Func<T, bool> predicate);
    Task<bool> ExistsAsync(Func<T, bool> predicate);
    void Save();
    Task SaveAsync();
}