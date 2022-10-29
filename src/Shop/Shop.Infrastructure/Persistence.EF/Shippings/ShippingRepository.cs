using Shop.Domain.ShippingAggregate;
using Shop.Domain.ShippingAggregate.Repository;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Shippings;

public class ShippingRepository : BaseRepository<Shipping>, IShippingRepository
{
    public ShippingRepository(ShopContext shopContext) : base(shopContext)
    {
    }
}