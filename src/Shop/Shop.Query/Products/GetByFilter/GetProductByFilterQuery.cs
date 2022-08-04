using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.ColorAggregate;
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
    private readonly DapperContext _dapperContext;

    public GetProductByFilterQueryHandler(ShopContext shopContext, DapperContext dapperContext)
    {
        _shopContext = shopContext;
        _dapperContext = dapperContext;
    }

    public async Task<ProductFilterResult> Handle(GetProductByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var conditions = "";

        if (@params.CategoryId != null && @params.CategoryId != 0)
            conditions += $"AND p.CategoryId = {@params.CategoryId}";

        if (!string.IsNullOrWhiteSpace(@params.Name))
            conditions += $"AND p.Name LIKE N'%{@params.Name}%'";

        if (!string.IsNullOrWhiteSpace(@params.EnglishName))
            conditions += $"AND p.EnglishName LIKE N'%{@params.EnglishName}%'";

        if (!string.IsNullOrWhiteSpace(@params.Slug))
            conditions += $"AND p.Slug LIKE N'%{@params.Slug}%'";

        if (@params.AverageScore != null)
            conditions += $"AND ps.Value >= {@params.AverageScore}";

        if (@params.MinPrice != null)
            conditions += $"AND i.Price >= {@params.MinPrice}";

        if (@params.MaxPrice != null)
            conditions += $"AND i.Price <= {@params.MaxPrice}";

        var skip = (@params.PageId - 1) * @params.Take;

        using var connection = _dapperContext.CreateConnection();
        var sql = $@"SELECT DISTINCT
                        p.Id, p.CategoryId, p.Name, p.EnglishName, p.Slug, p.Description, p.CreationDate,
						pi.Name AS MainImage,
                        i.Price AS LowestInventoryPrice, i.Quantity AS AllQuantityInStock,
						AVG(ps.Value) OVER(PARTITION BY p.Id) AS AverageScore,
                        c.Id, c.Name, c.Code, c.CreationDate
                    FROM {_dapperContext.Products} p
                    LEFT JOIN {_dapperContext.ProductImages} pi
                        ON p.Id = pi.ProductId
                    LEFT JOIN {_dapperContext.ProductScores} ps
                        ON p.Id = ps.ProductId
                    LEFT JOIN {_dapperContext.SellerInventories} i
                        ON p.Id = i.ProductId
                    LEFT JOIN {_dapperContext.Colors} c
                        ON i.ColorId = c.Id
                    WHERE 1 = 1 {conditions} ORDER BY p.Id offset @skip ROWS FETCH NEXT @take ROWS ONLY";

        var result = await connection
            .QueryAsync<ProductFilterDto, Color, ProductFilterDto>(sql, (productFilterDto, color) =>
            {
                productFilterDto.Colors.Add(color);
                return productFilterDto;
            }, param: new { skip, take = @params.Take }, splitOn: "Id");

        var groupedQueryResult = result.GroupBy(p => p.Id).Select(productGroup =>
        {
            var firstItem = productGroup.First();
            var colorList = productGroup.Select(p => p.Colors.Single()).DistinctBy(c => c.Code).ToList();
            firstItem.Colors = colorList;
            firstItem.LowestInventoryPrice = productGroup.OrderBy(p => p.LowestInventoryPrice).First().LowestInventoryPrice;
            firstItem.HighestInventoryPrice = productGroup.OrderBy(p => p.LowestInventoryPrice).Last().LowestInventoryPrice;
            firstItem.AllQuantityInStock = productGroup.Sum(p => p.AllQuantityInStock);
            firstItem.AverageScore = productGroup.Select(p => p.AverageScore).First();
            return firstItem;
        }).ToList();

        var prices = await _shopContext.Sellers
            .Select(s => new ProductFilterResult
            {
                HighestProductPrice = s.Inventories.OrderByDescending(i => i.Price.Value).First().Price.Value
            }).ToListAsync(cancellationToken);

        return new ProductFilterResult
        {
            Data = groupedQueryResult,
            FilterParam = @params,
            HighestProductPrice = prices.OrderByDescending(r => r.HighestProductPrice).First().HighestProductPrice
        };
    }
}