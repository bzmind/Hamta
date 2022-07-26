using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Sellers._DTOs;
using Shop.Query.Sellers._Mappers;

namespace Shop.Query.Sellers.Inventories.GetById;

public record GetSellerInventoryByIdQuery(long InventoryId) : IBaseQuery<SellerInventoryDto>;

public class GetSellerInventoryByIdQueryHandler : IBaseQueryHandler<GetSellerInventoryByIdQuery, SellerInventoryDto>
{
    private readonly ShopContext _shopContext;

    public GetSellerInventoryByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<SellerInventoryDto> Handle(GetSellerInventoryByIdQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _shopContext.Sellers
            .Select(seller => seller.Inventories.FirstOrDefault(inventory => inventory.Id == request.InventoryId))
            .FirstOrDefaultAsync(cancellationToken);

        return inventory.MapToSellerInventoryDto();
    }
}