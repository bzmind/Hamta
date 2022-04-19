using Common.Domain.BaseClasses;
using Common.Domain.Repository;
using Microsoft.EntityFrameworkCore;
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
        return _shopContext.Set<TEntity>().FirstOrDefault(t => t.Id == id);
    }

    public async Task<TEntity?> GetAsync(long id)
    {
        return await _shopContext.Set<TEntity>().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TEntity?> GetAsTrackingAsync(long id)
    {
        return await _shopContext.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.Id == id);
    }

    public void Add(TEntity entity)
    {
        _shopContext.Add(entity);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _shopContext.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _shopContext.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _shopContext.Remove(entity);
    }

    public bool Exists(Func<TEntity, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(Func<TEntity, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}