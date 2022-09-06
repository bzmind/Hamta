using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Sellers._DTOs;
using Shop.Query.Sellers._Mappers;

namespace Shop.Query.Sellers.Inventories.GetByFilter;

public class GetSellerInventoriesByFilterQuery :
    BaseFilterQuery<SellerInventoryFilterResult, SellerInventoryFilterParams>
{
    public GetSellerInventoriesByFilterQuery(SellerInventoryFilterParams filterParams) : base(filterParams)
    {
    }
}

public class GetSellerInventoriesByFilterQueryHandler :
    IBaseQueryHandler<GetSellerInventoriesByFilterQuery, SellerInventoryFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetSellerInventoriesByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<SellerInventoryFilterResult> Handle(GetSellerInventoriesByFilterQuery request,
        CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var query = _shopContext.Sellers
            .Where(seller => seller.UserId == @params.UserId)
            .SelectMany(seller => seller.Inventories)
            .Join(
                _shopContext.Products,
                inventory => inventory.ProductId,
                product => product.Id,
                (inventory, product) => new { inventory, product })
            .GroupJoin(
                _shopContext.Colors,
                tables => tables.inventory.ColorId,
                color => color.Id,
                (tables, colors) => new { tables, colors })
            .SelectMany(
                tables => tables.colors.DefaultIfEmpty(),
                (tables, color) => new { tables.tables, color })
            .OrderByDescending(tables => tables.tables.inventory.CreationDate)
            .AsQueryable();

        if (@params.ProductId != null)
            query = query.Where(tables => tables.tables.inventory.ProductId == @params.ProductId);

        if (@params.MinQuantity != null)
            query = query.Where(tables => tables.tables.inventory.Quantity >= @params.MinQuantity);

        if (@params.MaxQuantity != null)
            query = query.Where(tables => tables.tables.inventory.Quantity <= @params.MaxQuantity);

        if (@params.MinPrice != null)
            query = query.Where(tables => tables.tables.inventory.Price.Value >= @params.MinPrice);

        if (@params.MaxPrice != null)
            query = query.Where(tables => tables.tables.inventory.Price.Value <= @params.MaxPrice);

        if (@params.MinDiscountPercentage != null)
            query = query.Where(tables => tables.tables.inventory.DiscountPercentage >= @params.MinDiscountPercentage);

        if (@params.MaxDiscountPercentage != null)
            query = query.Where(tables => tables.tables.inventory.DiscountPercentage <= @params.MaxDiscountPercentage);

        if (@params.IsAvailable != null)
            query = query.Where(tables => tables.tables.inventory.IsAvailable == @params.IsAvailable);

        if (@params.IsDiscounted != null)
            query = query.Where(tables => tables.tables.inventory.IsDiscounted == @params.IsDiscounted);

        var skip = (@params.PageId - 1) * @params.Take;

        return new SellerInventoryFilterResult
        {
            Data = await query
                .Skip(skip)
                .Take(@params.Take)
                .Select(tables => tables.tables.inventory.MapToSellerInventoryDto(tables.color, tables.tables.product))
                .ToListAsync(cancellationToken),

            FilterParam = @params
        };
    }
}