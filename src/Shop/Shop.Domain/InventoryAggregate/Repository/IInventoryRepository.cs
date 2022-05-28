using Common.Domain.Repository;
using Shop.Domain.OrderAggregate;

namespace Shop.Domain.InventoryAggregate.Repository;

public interface IInventoryRepository : IBaseRepository<Inventory>
{
    Task<List<Inventory>> GetInventoriesForOrderItems(List<OrderItem> orderItems);
}