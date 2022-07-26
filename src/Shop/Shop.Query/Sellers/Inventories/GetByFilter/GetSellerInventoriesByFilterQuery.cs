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
            .SelectMany(seller => seller.Inventories)
            .OrderByDescending(inventory => inventory.CreationDate)
            .AsQueryable();

        if (@params.ProductId != null)
            query = query.Where(i => i.ProductId == @params.ProductId);
        
        if (@params.StartQuantity != null)
            query = query.Where(i => i.Quantity >= @params.StartQuantity);

        if (@params.EndQuantity != null)
            query = query.Where(i => i.Quantity <= @params.EndQuantity);

        if (@params.StartPrice != null)
            query = query.Where(i => i.Price.Value >= @params.StartPrice);

        if (@params.EndPrice != null)
            query = query.Where(i => i.Price.Value <= @params.EndPrice);

        if (@params.StartDiscountPercentage != null)
            query = query.Where(i => i.DiscountPercentage >= @params.StartDiscountPercentage);

        if (@params.EndDiscountPercentage != null)
            query = query.Where(i => i.DiscountPercentage <= @params.EndDiscountPercentage);

        if (@params.IsAvailable != null)
            query = query.Where(i => i.IsAvailable == @params.IsAvailable);

        if (@params.IsDiscounted != null)
            query = query.Where(i => i.IsDiscounted == @params.IsDiscounted);

        var skip = (@params.PageId - 1) * @params.Take;

        return new SellerInventoryFilterResult
        {
            Data = await query
                .Skip(skip)
                .Take(@params.Take)
                .Select(i => i.MapToSellerInventoryDto())
                .ToListAsync(cancellationToken),

            FilterParam = @params
        };
    }
}