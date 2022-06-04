using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Colors._DTOs;
using Shop.Query.Colors._Mappers;

namespace Shop.Query.Colors.GetList;

public class GetColorByFilterQuery : BaseFilterQuery<ColorFilterResult, ColorFilterParam>
{
    public GetColorByFilterQuery(ColorFilterParam filterParams) : base(filterParams)
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
        var @params = request.FilterParams;

        var query = _shopContext.Colors.OrderByDescending(c => c.CreationDate).AsQueryable();

        if (!string.IsNullOrWhiteSpace(@params.Name))
            query = query.Where(c => c.Name.Contains(@params.Name));

        if (!string.IsNullOrWhiteSpace(@params.Code))
            query = query.Where(c => c.Code.Contains(@params.Code));

        var skip = (@params.PageId - 1) * @params.Take;

        return new ColorFilterResult
        {
            Data = await query
                .Skip(skip)
                .Take(@params.Take)
                .Select(c => c.MapToColorDto())
                .ToListAsync(cancellationToken),

            FilterParam = @params
        };
    }
}