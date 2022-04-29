using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Products._DTOs;
using Shop.Query.Products._Mappers;

namespace Shop.Query.Products.GetByFilter;

public class GetProductByFilterQuery : BaseFilterQuery<ProductFilterResult, ProductFilterParam>
{
    public GetProductByFilterQuery(ProductFilterParam filterParams) : base(filterParams)
    {
    }
}

public class GetProductByFilterQueryHandler : IBaseQueryHandler<GetProductByFilterQuery, ProductFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetProductByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<ProductFilterResult> Handle(GetProductByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var query = _shopContext.Products.OrderByDescending(p => p.CreationDate).AsQueryable();

        if (@params.CategoryId != null)
            query = query.Where(p => p.CategoryId == @params.CategoryId);

        if (!string.IsNullOrWhiteSpace(@params.Name))
            query = query.Where(p => p.Name.Contains(@params.Name));

        if (!string.IsNullOrWhiteSpace(@params.EnglishName))
            query = query.Where(p => p.EnglishName != null && p.EnglishName.Contains(@params.EnglishName));

        if (!string.IsNullOrWhiteSpace(@params.Slug))
            query = query.Where(p => p.Slug.Contains(@params.Slug));

        if (@params.AverageScore != null)
            query = query.Where(p => p.Scores.Average(s => s.Value) >= @params.AverageScore);

        var skip = (@params.PageId - 1) * @params.Take;

        return new ProductFilterResult()
        {
            Data = await query
                .Skip(skip)
                .Take(@params.Take)
                .Select(p => p.MapToProductDto())
                .ToListAsync(cancellationToken),

            FilterParam = @params
        };
    }
}