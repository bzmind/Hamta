using Common.Domain.BaseClasses;
using Common.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using System.Linq.Expressions;

namespace Shop.Infrastructure.BaseClasses;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseAggregateRoot
{
    protected readonly ShopContext ShopContext;

    public BaseRepository(ShopContext shopContext)
    {
        ShopContext = shopContext;
    }

    public TEntity? Get(long id)
    {
        return ShopContext.Set<TEntity>().FirstOrDefault(t => t.Id == id);
    }

    public async Task<TEntity?> GetAsync(long id)
    {
        return await ShopContext.Set<TEntity>().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TEntity?> GetAsTrackingAsync(long id)
    {
        return await ShopContext.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.Id == id);
    }

    public void Add(TEntity entity)
    {
        ShopContext.Set<TEntity>().Add(entity);
    }

    public async Task AddAsync(TEntity entity)
    {
        await ShopContext.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        ShopContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        ShopContext.Set<TEntity>().Remove(entity);
    }

    public bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        return ShopContext.Set<TEntity>().Any(expression);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await ShopContext.Set<TEntity>().AnyAsync(expression);
    }

    public async Task SaveAsync()
    {
        await ShopContext.SaveChangesAsync();
    }
}