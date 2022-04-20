using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Orders;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ShopContext context) : base(context)
    {
    }

    public async Task<Order?> GetOrderByCustomerIdAsTracking(long customerId)
    {
        return await Context.Set<Order>().AsTracking().FirstOrDefaultAsync(o => o.CustomerId == customerId);
    }
}