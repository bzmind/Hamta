using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Products._DTOs;
using Shop.Query.Products._Mappers;

namespace Shop.Query.Products.GetByFilter;

public class GetProductByFilterQuery : BaseFilterQuery<ProductFilterResult, ProductFilterParams>
{
    public GetProductByFilterQuery(ProductFilterParams filterParams) : base(filterParams)
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
                _shopContext.Sellers.SelectMany(seller => seller.Inventories),
                p => p.Id,
                i => i.ProductId,
                (product, inventory) => new { product, inventory })
            .Join(
                _shopContext.Colors,
                t => t.inventory.ColorId,
                c => c.Id,
                (tables, color) => new
                {
                    tables.product,
                    tables.inventory,
                    color
                })
            .AsQueryable();

        if (@params.CategoryId != null)
            query = query.Where(tables => tables.product.CategoryId == @params.CategoryId);

        if (!string.IsNullOrWhiteSpace(@params.Name))
            query = query.Where(tables => tables.product.Name.Contains(@params.Name));

        if (!string.IsNullOrWhiteSpace(@params.EnglishName))
            query = query.Where(tables => tables.product.EnglishName != null &&
                                          tables.product.EnglishName.Contains(@params.EnglishName));

        if (!string.IsNullOrWhiteSpace(@params.Slug))
            query = query.Where(tables => tables.product.Slug.Contains(@params.Slug));

        if (@params.AverageScore != null)
            query = query.Where(tables => tables.product.AverageScore >= @params.AverageScore);

        var skip = (@params.PageId - 1) * @params.Take;

        var finalQuery = await query
            .Skip(skip)
            .Take(@params.Take)
            .ToListAsync(cancellationToken);

        var productListDtos = finalQuery
            .Select(t => t.product.MapToProductListDto(t.inventory).SetProductListDtoColors(t.color));

        var groupedQueryResult = productListDtos.GroupBy(p => p.Id).Select(productGroup =>
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