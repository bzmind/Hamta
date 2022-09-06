using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.ColorAggregate;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Products._DTOs;

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
        var categoryHasChanged = @params.OldCategoryId != @params.CategoryId;

        var tables = _shopContext.Sellers
            .SelectMany(s => s.Inventories)
            .Join(
                _shopContext.Products,
                inventory => inventory.ProductId,
                product => product.Id,
                (inventory, product) => new { inventory, product })
            .Join(
                _shopContext.Categories,
                tables => tables.product.CategoryId,
                category => category.Id,
                (tables, category) => new { tables, category })
            .AsQueryable();

        if (@params.CategoryId != null && @params.CategoryId != 0)
            tables = tables.Where(t => t.tables.product.CategoryId == @params.CategoryId);

        var tablesResult = await tables.ToListAsync(cancellationToken);

        int highestPriceInCategory;
        if (tablesResult.Any())
            highestPriceInCategory = tablesResult.Max(t => t.tables.inventory.Price.Value);
        else
            highestPriceInCategory = @params.MaxPrice.Value;

        if (categoryHasChanged)
            @params.MaxPrice = highestPriceInCategory;

        var conditions = "";
        var joinWithCategories = "";

        if (@params.CategoryId != null && @params.CategoryId != 0)
            joinWithCategories = @"JOIN category_children cc
                                       ON p.CategoryId = cc.Id";

        if (!string.IsNullOrWhiteSpace(@params.Name))
            conditions += $" AND p.Name LIKE N'%{@params.Name}%'";

        if (!string.IsNullOrWhiteSpace(@params.EnglishName))
            conditions += $" AND p.EnglishName LIKE N'%{@params.EnglishName}%'";

        if (!string.IsNullOrWhiteSpace(@params.Slug))
            conditions += $" AND p.Slug LIKE N'%{@params.Slug}%'";

        if (@params.AverageScore != null)
            conditions += $" AND ps.Value >= {@params.AverageScore}";

        if (@params.MinPrice != null)
            conditions += $" AND i.Price >= {@params.MinPrice} OR i.Price IS NULL";

        if (@params.MaxPrice != null)
            conditions += $" AND i.Price <= {@params.MaxPrice} OR i.Price IS NULL";

        var skip = (@params.PageId - 1) * @params.Take;

        using var connection = _dapperContext.CreateConnection();
        var sql = $@"WITH category_children AS (
                       SELECT Id, ParentId, Title
                       FROM category.Categories
                       WHERE Id = @CategoryId
                       UNION ALL
                       SELECT c.Id, c.ParentId, c.Title
                       FROM category.Categories c
                       JOIN category_children cc
                    		ON cc.Id = c.ParentId
                    )
                    SELECT DISTINCT
                    	p.Id, p.Name, p.EnglishName, p.Slug, p.MainImage, p.CreationDate,
                    	AVG(ps.Value) OVER (PARTITION by p.Id) AS AverageScore,
                        i.Id AS InventoryId,
                    	MIN(i.Price) OVER (PARTITION by p.Id) AS LowestInventoryPrice,
                    	MAX(i.Price) OVER (PARTITION by p.Id) AS HighestInventoryPrice,
                    	q.Quantity AS AllQuantityInStock,
                        c.Id, c.Name, c.Code, c.CreationDate
                    FROM
                    	(
                    		SELECT DISTINCT
                                Id, Name, EnglishName, Slug, MainImage, CreationDate, CategoryId
                    		FROM {_dapperContext.Products}
		                    ORDER BY CreationDate DESC
                    		OFFSET @skip ROWS
                    		FETCH NEXT @take ROWS ONLY
                    	) AS p
                    {joinWithCategories}
                    LEFT JOIN {_dapperContext.ProductScores} ps
                    	ON p.Id = ps.ProductId
                    LEFT JOIN {_dapperContext.SellerInventories} i
                    	ON p.Id = i.ProductId
                    LEFT JOIN {_dapperContext.Colors} c
                    	ON c.Id = i.ColorId
                    LEFT JOIN
                    	(
                    		SELECT ProductId, SUM(Quantity) AS Quantity
                    		FROM seller.Inventories
                    		GROUP BY ProductId
                    	) AS q
                    	ON p.Id = q.ProductId
                    WHERE 1 = 1 {conditions}
                    ORDER BY p.CreationDate DESC";

        var result = await connection
            .QueryAsync<ProductFilterDto, Color, ProductFilterDto>(sql, (productFilterDto, color) =>
            {
                productFilterDto.Colors.Add(color);
                return productFilterDto;
            }, param: new { skip, take = @params.Take, @params.CategoryId }, splitOn: "Id");

        var groupedQueryResult = result.GroupBy(p => p.Id).Select(productGroup =>
        {
            var firstItem = productGroup.First();
            var colorList = productGroup.Select(p => p.Colors.FirstOrDefault())
                .DistinctBy(c => c?.Code).OrderBy(c => c?.Id).ToList();
            firstItem.Colors = colorList;
            firstItem.LowestInventoryPrice = productGroup.First().LowestInventoryPrice;
            firstItem.HighestInventoryPrice = productGroup.First().HighestInventoryPrice;
            firstItem.AllQuantityInStock = productGroup.First().AllQuantityInStock;
            firstItem.AverageScore = productGroup.First().AverageScore;
            return firstItem;
        }).ToList();

        return new ProductFilterResult
        {
            Data = groupedQueryResult,
            FilterParam = @params,
            HighestPriceInCategory = highestPriceInCategory
        };
    }
}