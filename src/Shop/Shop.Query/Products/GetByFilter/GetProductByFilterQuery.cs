using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Dapper;
using Shop.Domain.ColorAggregate;
using Shop.Infrastructure;
using Shop.Query.Products._DTOs;
using OrderBy = Shop.Query.Products._DTOs.OrderBy;

namespace Shop.Query.Products.GetByFilter;

public class GetProductByFilterQuery : BaseFilterQuery<ProductFilterResult, ProductFilterParams>
{
    public GetProductByFilterQuery(ProductFilterParams filterParams) : base(filterParams)
    {
    }
}

public class GetProductByFilterQueryHandler : IBaseQueryHandler<GetProductByFilterQuery, ProductFilterResult>
{
    private readonly DapperContext _dapperContext;

    public GetProductByFilterQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<ProductFilterResult> Handle(GetProductByFilterQuery request,
        CancellationToken cancellationToken)
    {
        var @params = request.FilterFilterParams;
        var skip = (@params.PageId - 1) * @params.Take;

        var categoryHasChanged = false;
        if (@params.OldCategoryId != null)
            categoryHasChanged = @params.OldCategoryId != @params.CategoryId;

        var highestPriceWhere = "";
        if (@params.CategoryId != null && @params.CategoryId != 0)
            highestPriceWhere = $"WHERE Id = {@params.CategoryId}";

        using var highestPriceConnection = _dapperContext.CreateConnection();
        var highestPriceSql = $@"
            WITH category_children AS (
                SELECT Id, ParentId, Title
                FROM {_dapperContext.Categories}
                {highestPriceWhere}
                UNION ALL
                SELECT c.Id, c.ParentId, c.Title
                FROM {_dapperContext.Categories} c
                JOIN category_children cc
                	ON cc.Id = c.ParentId
            )
            SELECT TOP(1) MAX(si.Price - si.Price * si.DiscountPercentage / 100) AS HighestPriceInCategory
            FROM {_dapperContext.Products} p
            JOIN category_children cc
            	ON p.CategoryId = cc.Id
            JOIN {_dapperContext.SellerInventories} si
            	ON si.ProductId = p.Id
            GROUP BY p.Id
            ORDER BY MAX(si.Price - si.Price * si.DiscountPercentage / 100) DESC";

        var highestPrice = await highestPriceConnection.QueryFirstOrDefaultAsync<int>(highestPriceSql);

        int highestPriceInCategory = 0;

        if (highestPrice != 0)
        {
            highestPriceInCategory = highestPrice;
        }
        else if (@params.MaxPrice == null && highestPriceWhere != "")
        {
            highestPriceInCategory = await highestPriceConnection
                .QueryFirstOrDefaultAsync<int>(highestPriceSql.Replace(highestPriceWhere, ""));
        }

        if (categoryHasChanged)
            @params.MaxPrice = highestPriceInCategory;

        var conditions = "";
        var joinWithCategories = "";
        var orderBy = "";
        const string discountedPrice = "i.Price - i.Price * i.DiscountPercentage / 100";

        if (@params.CategoryId != null && @params.CategoryId != 0)
            joinWithCategories = @"JOIN category_children cc ON p.CategoryId = cc.Id";

        if (!string.IsNullOrWhiteSpace(@params.Name))
            conditions += $" AND (p.Name LIKE N'%{@params.Name}%' OR p.EnglishName LIKE N'%{@params.Name}%')";

        if (!string.IsNullOrWhiteSpace(@params.Slug))
            conditions += $" AND p.Slug LIKE N'%{@params.Slug}%'";

        if (@params.AverageScore != null)
            conditions += $" AND c.Score >= {@params.AverageScore}";

        if (@params.MinPrice != null)
            conditions += $" AND ({discountedPrice} >= {@params.MinPrice} OR i.Price IS NULL)";

        if (@params.MaxPrice != null)
            conditions += $" AND ({discountedPrice} <= {@params.MaxPrice} OR i.Price IS NULL)";

        if (@params.MinDiscountPercentage != null)
            conditions += $" AND (i.DiscountPercentage >= {@params.MinDiscountPercentage} " +
                          "OR i.DiscountPercentage IS NULL)";

        if (@params.MaxDiscountPercentage != null)
            conditions += $" AND (i.DiscountPercentage <= {@params.MaxDiscountPercentage} " +
                          "OR i.DiscountPercentage IS NULL)";

        orderBy = @params.OrderBy switch
        {
            OrderBy.Cheapest => "ORDER BY i.Price",
            OrderBy.MostExpensive => "ORDER BY i.Price DESC",
            OrderBy.Latest => "ORDER BY p.Id DESC",
            _ => "ORDER BY AverageScore DESC"
        };

        using var connection = _dapperContext.CreateConnection();
        var sql = $@"
            WITH category_children AS (
                SELECT Id, ParentId, Title
                FROM {_dapperContext.Categories}
                WHERE Id = @CategoryId

                UNION ALL

                SELECT c.Id, c.ParentId, c.Title
                FROM {_dapperContext.Categories} c
                JOIN category_children cc
                	ON cc.Id = c.ParentId
            )
            SELECT DISTINCT
            	p.Id, p.Name, p.EnglishName, p.Slug, p.MainImage, p.CreationDate,
            	AVG(cmnt.Score) OVER (PARTITION BY p.Id) AS AverageScore,
                i.Id AS InventoryId,
            	MIN(i.Price) OVER (PARTITION BY p.Id) AS LowestInventoryPrice,
            	MAX(i.Price) OVER (PARTITION BY p.Id) AS HighestInventoryPrice,
            	q.Quantity AS InventoryQuantity,
                c.Id, c.Name, c.Code, c.CreationDate
            FROM (
            	SELECT DISTINCT
                    p.Id, p.Name, p.EnglishName, p.Slug, p.MainImage, p.CreationDate, p.CategoryId,
					MIN(i.Price) OVER (PARTITION BY p.Id) AS Price,
                    AVG(c.Score) OVER (PARTITION BY p.Id) AS AverageScore
            	FROM {_dapperContext.Products} p
                LEFT JOIN {_dapperContext.Comments} c
                	ON p.Id = c.ProductId
                LEFT JOIN {_dapperContext.SellerInventories} i
                	ON p.Id = i.ProductId
                WHERE 1 = 1 {conditions}
                {orderBy}
            	OFFSET @skip ROWS
            	FETCH NEXT @take ROWS ONLY
            ) AS p
            {joinWithCategories}
            LEFT JOIN {_dapperContext.Comments} cmnt
            	ON p.Id = cmnt.ProductId
            LEFT JOIN {_dapperContext.SellerInventories} i
            	ON p.Id = i.ProductId
            LEFT JOIN {_dapperContext.Colors} c
            	ON c.Id = i.ColorId
            LEFT JOIN (
            	SELECT ProductId, SUM(Quantity) AS Quantity
            	FROM {_dapperContext.SellerInventories}
            	GROUP BY ProductId
            ) AS q
            	ON p.Id = q.ProductId
            WHERE 1 = 1 {conditions}
            {orderBy}";

        var query = await connection
            .QueryAsync<ProductFilterDto, Color, ProductFilterDto>(sql, (productFilterDto, color) =>
            {
                if (color != null)
                    productFilterDto.Colors.Add(color);
                return productFilterDto;
            }, param: new { skip, take = @params.Take, @params.CategoryId }, splitOn: "Id");

        var groupedQueryResult = query.GroupBy(p => p.Id).Select(productGroup =>
        {
            var firstItem = productGroup.OrderBy(p => p.LowestInventoryPrice).First();
            var colorList = productGroup.Select(p => p.Colors.FirstOrDefault())
                .DistinctBy(c => c?.Code).OrderBy(c => c?.Id).ToList();
            firstItem.Colors = colorList;
            return firstItem;
        }).ToList();

        switch (@params.OrderBy)
        {
            case OrderBy.Cheapest:
                groupedQueryResult = groupedQueryResult.OrderBy(p => p.LowestInventoryPrice).ToList();
                break;

            case OrderBy.MostExpensive:
                groupedQueryResult = groupedQueryResult.OrderByDescending(p => p.LowestInventoryPrice).ToList();
                break;

            case OrderBy.Latest:
                groupedQueryResult = groupedQueryResult.OrderByDescending(p => p.Id).ToList();
                break;

            default:
                groupedQueryResult = groupedQueryResult.OrderByDescending(p => p.AverageScore).ToList();
                break;
        }

        var countSql = $@"
            WITH category_children AS (
                SELECT Id, ParentId, Title
                FROM {_dapperContext.Categories}
                WHERE Id = @CategoryId

                UNION ALL

                SELECT c.Id, c.ParentId, c.Title
                FROM {_dapperContext.Categories} c
                JOIN category_children cc
                	ON cc.Id = c.ParentId
            )
            SELECT COUNT(A.Id)
			FROM (
                SELECT DISTINCT
                    ROW_NUMBER() OVER (PARTITION BY p.Id ORDER BY p.Id) AS RN,
                	p.Id, p.Name, p.EnglishName, p.Slug, p.MainImage, p.CreationDate,
                	AVG(cmnt.Score) OVER (PARTITION BY p.Id) AS AverageScore,
                    i.Id AS InventoryId,
                	MIN(i.Price) OVER (PARTITION BY p.Id) AS LowestInventoryPrice,
                	MAX(i.Price) OVER (PARTITION BY p.Id) AS HighestInventoryPrice,
                	q.Quantity AS InventoryQuantity,
                    c.Id AS ColorId, c.Name AS ColorName, c.Code AS ColorCode, c.CreationDate AS ColorCreationDate
                FROM (
                	SELECT DISTINCT
                        p.Id, p.Name, p.EnglishName, p.Slug, p.MainImage, p.CreationDate, p.CategoryId,
			    		MIN(i.Price) OVER (PARTITION BY p.Id) AS Price,
                        AVG(c.Score) OVER (PARTITION BY p.Id) AS AverageScore
                	FROM {_dapperContext.Products} p
                    LEFT JOIN {_dapperContext.Comments} c
                    	ON p.Id = c.ProductId
                    LEFT JOIN {_dapperContext.SellerInventories} i
                    	ON p.Id = i.ProductId
                ) AS p
                {joinWithCategories}
                LEFT JOIN {_dapperContext.Comments} cmnt
                	ON p.Id = cmnt.ProductId
                LEFT JOIN {_dapperContext.SellerInventories} i
                	ON p.Id = i.ProductId
                LEFT JOIN {_dapperContext.Colors} c
                	ON c.Id = i.ColorId
                LEFT JOIN (
                	SELECT ProductId, SUM(Quantity) AS Quantity
                	FROM {_dapperContext.SellerInventories}
                	GROUP BY ProductId
                ) AS q
                	ON p.Id = q.ProductId
                WHERE 1 = 1 {conditions}
            ) AS A
			WHERE A.RN = 1";

        var count = await connection.QueryFirstOrDefaultAsync<int>(countSql, new
        {
            skip,
            take = @params.Take,
            @params.CategoryId
        });

        var model = new ProductFilterResult
        {
            Data = groupedQueryResult,
            FilterParams = @params,
            HighestPriceInCategory = highestPriceInCategory
        };
        model.GeneratePaging(count, @params.Take, @params.PageId);
        return model;
    }
}