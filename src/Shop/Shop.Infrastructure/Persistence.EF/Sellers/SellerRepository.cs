using Common.Domain.ValueObjects;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAggregate;
using Shop.Domain.SellerAggregate;
using Shop.Domain.SellerAggregate.Repository;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Sellers;

public class SellerRepository : BaseRepository<Seller>, ISellerRepository
{
    private readonly DapperContext _dapperContext;

    public SellerRepository(ShopContext context, DapperContext dapperContext) : base(context)
    {
        _dapperContext = dapperContext;
    }

    public async Task<Seller?> GetSellerByUserIdAsTrackingAsync(long userId)
    {
        return await Context.Sellers.AsTracking().FirstOrDefaultAsync(seller => seller.UserId == userId);
    }

    public async Task<SellerInventory?> GetInventoryByIdAsync(long inventoryId)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $@"
            SELECT
                Id, CreationDate, ProductId, SellerId, ColorId, Quantity, IsAvailable,
                IsDiscounted, DiscountPercentage, Price AS Value
            FROM {_dapperContext.SellerInventories}
            WHERE Id = @InventoryId";

        var query = await connection.QueryAsync<SellerInventory, Money, SellerInventory>(sql, (inventory, money) =>
        {
            inventory.Edit(inventory.ProductId, inventory.Quantity, money.Value,
                inventory.ColorId, inventory.DiscountPercentage);
            return inventory;
        }, new { InventoryId = inventoryId }, splitOn: "Value");

        return query.SingleOrDefault();
    }

    public async Task<List<SellerInventory?>> GetOrderItemInventoriesAsTrackingAsync(List<OrderItem> orderItems)
    {
        var inventoryIds = new List<long>();
        orderItems.ForEach(orderItem =>
        {
            inventoryIds.Add(orderItem.InventoryId);
        });

        // 99% Doesn't work, like the above, it doesn't let you only Select the inventories from the sellers,
        // you have to get all of that Seller object, not part of it.
        return await Context.Sellers.AsTracking()
            .Select(seller => seller.Inventories.FirstOrDefault(inventory => inventoryIds.Contains(inventory.Id)))
            .ToListAsync();
    }
}