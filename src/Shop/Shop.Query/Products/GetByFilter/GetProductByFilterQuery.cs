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

        var query = _shopContext.Products
            .OrderByDescending(p => p.CreationDate)
            .Join(
                _shopContext.Inventories,
                p => p.Id,
                i => i.ProductId,
                (product, inventory) => product.MapToProductListDto(inventory))
            .Join(
                _shopContext.Colors,
                p => p.ColorId,
                c => c.Id,
                (productListDto, color) => productListDto.SetProductListDtoColors(color))
            .AsQueryable();

        if (@params.CategoryId != null)
            query = query.Where(p => p.CategoryId == @params.CategoryId);

        if (!string.IsNullOrWhiteSpace(@params.Name))
            query = query.Where(p => p.Name.Contains(@params.Name));

        if (!string.IsNullOrWhiteSpace(@params.EnglishName))
            query = query.Where(p => p.EnglishName != null && p.EnglishName.Contains(@params.EnglishName));

        if (!string.IsNullOrWhiteSpace(@params.Slug))
            query = query.Where(p => p.Slug.Contains(@params.Slug));

        if (@params.AverageScore != null)
            query = query.Where(p => p.AverageScore >= @params.AverageScore);

        var skip = (@params.PageId - 1) * @params.Take;

        var finalQuery = await query
            .Skip(skip)
            .Take(@params.Take)
            .ToListAsync(cancellationToken);

        var groupedQueryResult = finalQuery.GroupBy(p => p.Id).Select(productGroup =>
        {
            var firstItem = productGroup.First();
            firstItem.Colors = productGroup.Select(p => p.Colors.Single()).ToList();
            return firstItem;
        }).ToList();

        return new ProductFilterResult
        {
            Data = groupedQueryResult,
            FilterParam = @params
        };
    }
}