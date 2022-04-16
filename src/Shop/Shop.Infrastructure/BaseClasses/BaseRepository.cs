using Common.Domain.BaseClasses;
using Common.Domain.Repository;
using Shop.Infrastructure.Persistence.EF;

namespace Shop.Infrastructure.BaseClasses;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseAggregateRoot
{
    private readonly ShopContext _shopContext;

    public BaseRepository(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public TEntity? Get(long id)
    {
        _shopContext
    }

    public Task<TEntity?> GetAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetAsTrackingAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public bool Exists(Func<TEntity, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Func<TEntity, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}