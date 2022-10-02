using Common.Domain.Repository;
using Shop.Domain.OrderAggregate;

namespace Shop.Domain.SellerAggregate.Repository;

public interface ISellerRepository : IBaseRepository<Seller>
{
    Task<Seller?> GetSellerByUserIdAsTrackingAsync(long userId);
    Task<SellerInventory?> GetInventoryByIdAsync(long inventoryId);
    Task<List<SellerInventory?>> GetOrderItemInventoriesAsTrackingAsync(List<OrderItem> orderItems);
}