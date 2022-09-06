using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Sellers._DTOs;
using Shop.Query.Sellers._Mappers;

namespace Shop.Query.Sellers.GetById;

public record GetSellerByUserIdQuery(long UserId) : IBaseQuery<SellerDto?>;

public class GetSellerByUserIdQueryHandler : IBaseQueryHandler<GetSellerByUserIdQuery, SellerDto?>
{
    private readonly ShopContext _shopContext;

    public GetSellerByUserIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<SellerDto?> Handle(GetSellerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var seller = await _shopContext.Sellers
            .FirstOrDefaultAsync(seller => seller.UserId == request.UserId, cancellationToken);
        return seller.MapToSellerDto();
    }
}