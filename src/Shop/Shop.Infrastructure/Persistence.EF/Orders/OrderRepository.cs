using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Orders;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ShopContext shopContext) : base(shopContext)
    {
    }

    public async Task<Order?> GetOrderByUserIdAsTracking(long userId)
    {
        return await ShopContext.Orders.AsTracking()
            .FirstOrDefaultAsync(o => o.UserId == userId && o.Status == Order.OrderStatus.Pending);
    }
}