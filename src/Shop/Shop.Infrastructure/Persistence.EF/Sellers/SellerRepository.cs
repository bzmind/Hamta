using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAggregate;
using Shop.Domain.SellerAggregate;
using Shop.Domain.SellerAggregate.Repository;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Sellers;

public class SellerRepository : BaseRepository<Seller>, ISellerRepository
{
    public SellerRepository(ShopContext context) : base(context)
    {
    }

    public async Task<Seller?> GetSellerByUserIdAsTrackingAsync(long userId)
    {
        return await Context.Sellers.AsTracking().FirstOrDefaultAsync(seller => seller.UserId == userId);
    }

    public async Task<SellerInventory?> GetInventoryByIdAsTrackingAsync(long inventoryId)
    {
        return await Context.Sellers.AsTracking()
            .Select(seller => seller.Inventories.FirstOrDefault(inventory => inventory.Id == inventoryId))
            .FirstOrDefaultAsync();
    }

    public async Task<List<SellerInventory?>> GetOrderItemInventoriesAsTrackingAsync(List<OrderItem> orderItems)
    {
        var inventoryIds = new List<long>();
        orderItems.ForEach(orderItem =>
        {
            inventoryIds.Add(orderItem.InventoryId);
        });

        return await Context.Sellers.AsTracking()
            .Select(seller => seller.Inventories.FirstOrDefault(inventory => inventoryIds.Contains(inventory.Id)))
            .ToListAsync();
    }
}