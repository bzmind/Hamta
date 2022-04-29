using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Orders._DTOs;
using Shop.Query.Orders._Mappers;

namespace Shop.Query.Orders.GetByFilter;

public class GetOrderByFilterQuery : BaseFilterQuery<OrderFilterResult, OrderFilterParam>
{
    public GetOrderByFilterQuery(OrderFilterParam filterParams) : base(filterParams)
    {
    }
}

public class GetOrderByFilterQueryHandler : IBaseQueryHandler<GetOrderByFilterQuery, OrderFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetOrderByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<OrderFilterResult> Handle(GetOrderByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var query = _shopContext.Orders.OrderByDescending(o => o.CreationDate).AsQueryable();

        if (@params.CustomerId != null)
            query = query.Where(o => o.CustomerId == @params.CustomerId);

        if (@params.StartDate != null)
            query = query.Where(o => o.CreationDate >= @params.StartDate);

        if (@params.EndDate != null)
            query = query.Where(o => o.CreationDate <= @params.EndDate);

        if (@params.Status != null)
            query = query.Where(o => o.Status == @params.Status);

        var skip = (@params.PageId - 1) * @params.Take;

        return new OrderFilterResult()
        {
            Data = await query
                .Skip(skip)
                .Take(@params.Take)
                .Select(o => o.MapToOrderDto())
                .ToListAsync(cancellationToken),

            FilterParam = @params
        };
    }
}