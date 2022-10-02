using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Colors._DTOs;
using Shop.Query.Colors._Mappers;

namespace Shop.Query.Colors.GetByFilter;

public class GetColorByFilterQuery : BaseFilterQuery<ColorFilterResult, ColorFilterParams>
{
    public GetColorByFilterQuery(ColorFilterParams filterParams) : base(filterParams)
    {
    }
}

public class GetColorByFilterQueryHandler : IBaseQueryHandler<GetColorByFilterQuery, ColorFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetColorByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<ColorFilterResult> Handle(GetColorByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterFilterParams;

        var query = _shopContext.Colors.OrderBy(c => c.CreationDate).AsQueryable();

        if (!string.IsNullOrWhiteSpace(@params.Name))
            query = query.Where(c => c.Name.Contains(@params.Name));

        if (!string.IsNullOrWhiteSpace(@params.Code))
            query = query.Where(c => c.Code.Contains(@params.Code));

        if (@params.Take != 0)
            query = query.Take(@params.Take);

        var skip = (@params.PageId - 1) * @params.Take;

        var queryResult = await query
            .Skip(skip)
            .Select(c => c.MapToColorDto())
            .ToListAsync(cancellationToken);

        var model = new ColorFilterResult
        {
            Data = queryResult,
            FilterParams = @params
        };
        model.GeneratePaging(query.Count(), @params.Take, @params.PageId);
        return model;
    }
}