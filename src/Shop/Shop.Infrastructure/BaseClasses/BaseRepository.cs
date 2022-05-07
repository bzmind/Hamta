using System.Linq.Expressions;
using Common.Domain.BaseClasses;
using Common.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;

namespace Shop.Infrastructure.BaseClasses;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseAggregateRoot
{
    protected readonly ShopContext Context;

    public BaseRepository(ShopContext context)
    {
        Context = context;
    }

    public TEntity? Get(long id)
    {
        return Context.Set<TEntity>().FirstOrDefault(t => t.Id == id);
    }

    public async Task<TEntity?> GetAsync(long id)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TEntity?> GetAsTrackingAsync(long id)
    {
        return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.Id == id);
    }

    public void Add(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().Any(expression);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Context.Set<TEntity>().AnyAsync(expression);
    }

    public async Task SaveAsync()
    {
        await Context.SaveChangesAsync();
    }
}