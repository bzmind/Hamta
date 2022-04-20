using System.Linq.Expressions;
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
    bool Exists(Expression<Func<T, bool>> expression);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
    Task SaveAsync();
}