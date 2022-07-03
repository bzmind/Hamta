using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Shippings._DTOs;
using Shop.Query.Shippings._Mappers;

namespace Shop.Query.Shippings.GetById;

public record GetShippingByIdQuery(long ShippingId) : IBaseQuery<ShippingDto>;

public class GetShippingByIdQueryHandler : IBaseQueryHandler<GetShippingByIdQuery, ShippingDto>
{
    private readonly ShopContext _shopContext;

    public GetShippingByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<ShippingDto> Handle(GetShippingByIdQuery request, CancellationToken cancellationToken)
    {
        var shipping = await _shopContext.Shippings
            .FirstOrDefaultAsync(s => s.Id == request.ShippingId, cancellationToken);
        return shipping.MapToShippingDto();
    }
}