using Common.Domain.BaseClasses;

namespace Common.Domain.Repository;

public interface IBaseRepository<T> where T : BaseAggregateRoot
{
    Task<T> GetAsync(long id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}