using Common.Domain.Repository;

namespace Shop.Domain.OrderAggregate.Repository;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<Order?> GetOrderByUserIdAsTracking(long userId);
}