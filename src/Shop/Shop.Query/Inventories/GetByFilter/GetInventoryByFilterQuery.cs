using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Inventories._DTOs;
using Shop.Query.Inventories._Mappers;

namespace Shop.Query.Inventories.GetByFilter;

public class GetInventoryByFilterQuery : BaseFilterQuery<InventoryFilterResult, InventoryFilterParam>
{
    public GetInventoryByFilterQuery(InventoryFilterParam filterParams) : base(filterParams)
    {
    }
}

public class GetInventoryByFilterQueryHandler : IBaseQueryHandler<GetInventoryByFilterQuery, InventoryFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetInventoryByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<InventoryFilterResult> Handle(GetInventoryByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var query = _shopContext.Inventories.OrderByDescending(i => i.CreationDate).AsQueryable();
        
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

        return new InventoryFilterResult
        {
            Data = await query
                .Skip(skip)
                .Take(@params.Take)
                .Select(i => i.MapToInventoryDto())
                .ToListAsync(cancellationToken),

            FilterParam = @params
        };
    }
}