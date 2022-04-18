using Shop.Domain.OrderAggregate.Services;
using Shop.Domain.ShippingMethodAggregate;

namespace Shop.Application.Orders.Services;

public class OrderDomainService : IOrderDomainService
{
    public Shipping GetShippingInfo(long shippingMethodId)
    {
        throw new NotImplementedException();
    }
}