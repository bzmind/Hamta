using Shop.Domain.ShippingMethodAggregate;

namespace Shop.Domain.OrderAggregate.Services;

public interface IOrderDomainService
{
    Shipping GetShippingInfo(long shippingMethodId);
}