using Microsoft.EntityFrameworkCore;
using Shop.Domain.InventoryAggregate;
using Shop.Domain.InventoryAggregate.Repository;
using Shop.Domain.OrderAggregate;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Inventories;

public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
{
    public InventoryRepository(ShopContext context) : base(context)
    {
    }

    public async Task<List<Inventory>> GetInventoriesForOrderItems(List<OrderItem> orderItems)
    {
        List<long> ids = new List<long>();

        orderItems.ForEach(oi =>
        {
            ids.Add(oi.InventoryId);
        });

        var query = Context.Inventories.AsQueryable();

        orderItems.ForEach(_ =>
        {
            query = query.Where(i => ids.Contains(i.Id));
        });

        return await query.AsTracking().ToListAsync();
    }
}