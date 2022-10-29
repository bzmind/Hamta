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

    public SellerRepository(ShopContext shopContext, DapperContext dapperContext) : base(shopContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<Seller?> GetSellerByUserIdAsTrackingAsync(long userId)
    {
        return await ShopContext.Sellers.AsTracking().FirstOrDefaultAsync(seller => seller.UserId == userId);
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

    public async Task<List<SellerInventory?>> GetOrderItemInventoriesAsync(List<OrderItem> orderItems)
    {
        var inventoryIds = new List<long>();
        orderItems.ForEach(orderItem =>
        {
            inventoryIds.Add(orderItem.InventoryId);
        });

        var result = await ShopContext.Sellers.AsTracking()
            .Where(s => s.Inventories.Any(si => inventoryIds.Contains(si.Id)))
            .ToListAsync();

        return result.SelectMany(s => s.Inventories).ToList();
    }
}