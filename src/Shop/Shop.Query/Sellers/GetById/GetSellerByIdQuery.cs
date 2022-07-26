using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Sellers._DTOs;
using Shop.Query.Sellers._Mappers;

namespace Shop.Query.Sellers.GetById;

public record GetSellerByIdQuery(long UserId) : IBaseQuery<SellerDto?>;

public class GetSellerByIdQueryHandler : IBaseQueryHandler<GetSellerByIdQuery, SellerDto?>
{
    private readonly ShopContext _context;

    public GetSellerByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<SellerDto?> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
    {
        var seller = await _context.Sellers
            .FirstOrDefaultAsync(seller => seller.UserId == request.UserId, cancellationToken);
        return seller.MapToSellerDto();
    }
}