using Shop.Domain.InventoryAggregate;
using Shop.Domain.InventoryAggregate.Repository;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Inventories;

public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
{
    public InventoryRepository(ShopContext context) : base(context)
    {
    }
}