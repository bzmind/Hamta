using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Shippings._DTOs;
using Shop.Query.Shippings._Mappers;

namespace Shop.Query.Shippings.GetList;

public record GetShippingListQuery : IBaseQuery<List<ShippingDto>>;

public class GetShippingListQueryHandler : IBaseQueryHandler<GetShippingListQuery, List<ShippingDto>>
{
    private readonly ShopContext _shopContext;

    public GetShippingListQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<List<ShippingDto>> Handle(GetShippingListQuery request, CancellationToken cancellationToken)
    {
        var shipping =
            await _shopContext.Shippings.OrderByDescending(s => s.Id).ToListAsync(cancellationToken);

        return shipping.MapToShippingDto();
    }
}