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
        var tables = await _shopContext.Sellers
            .Select(seller => seller.Inventories.FirstOrDefault(inventory => inventory.Id == request.InventoryId))
            .GroupJoin(
                _shopContext.Colors,
                inventory => inventory.ColorId,
                color => color.Id,
                (inventory, colors) => new { inventory, colors })
            .SelectMany(
                tables => tables.colors.DefaultIfEmpty(),
                (tables, color) => new { tables.inventory, color })
            .Join(
                _shopContext.Products,
                tables => tables.inventory.ProductId,
                product => product.Id,
                (tables, product) => new { tables, product })
            .FirstOrDefaultAsync(cancellationToken);

        return tables.tables.inventory.MapToSellerInventoryDto(tables.tables.color, tables.product);
    }
}