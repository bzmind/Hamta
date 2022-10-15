using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Dapper;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.ColorAggregate;
using Shop.Infrastructure;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products.GetForShopByFilter;

public class GetProductForShopByFilterQuery : BaseFilterQuery<ProductForShopResult, ProductForShopFilterParams>
{
    public GetProductForShopByFilterQuery(ProductForShopFilterParams filterFilterParams) : base(filterFilterParams)
    {
    }
}

public class GetProductForShopByFilterQueryHandler : IBaseQueryHandler<GetProductForShopByFilterQuery,
    ProductForShopResult>
{
    private readonly DapperContext _dapperContext;

    public GetProductForShopByFilterQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<ProductForShopResult> Handle(GetProductForShopByFilterQuery request,
        CancellationToken cancellationToken)
    {
        var @params = request.FilterFilterParams;
        var skip = (@params.PageId - 1) * @params.Take;

        var categoryHasChanged = @params.OldCategoryId != @params.CategoryId;

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
            SELECT TOP(1) MAX(si.Price) As HighestPriceInCategory
            FROM {_dapperContext.Products} p
            JOIN category_children cc
            	ON p.CategoryId = cc.Id
            JOIN {_dapperContext.SellerInventories} si
            	ON si.ProductId = p.Id
            GROUP BY p.Id
            ORDER BY MAX(si.Price) DESC";

        var highestPrice = await highestPriceConnection.QueryFirstOrDefaultAsync<int>(highestPriceSql);

        int highestPriceInCategory;
        if (highestPrice != 0)
            highestPriceInCategory = highestPrice;
        else
            highestPriceInCategory = @params.MaxPrice
                                     ?? await highestPriceConnection.QueryFirstOrDefaultAsync<int>(
                                         highestPriceSql.Replace(highestPriceWhere, ""));

        if (categoryHasChanged)
            @params.MaxPrice = highestPriceInCategory;

        var conditions = "";
        var joinWithCategories = "";
        var joinWithCategorySpecifications = "";
        var categoryWhereCondition = "";
        var totalPrice = "p.Price - p.Price * DiscountPercentage / 100";

        if (@params.CategoryId != null && @params.CategoryId != 0)
        {
            joinWithCategories = @"
                JOIN category_children cc
                    ON p.CategoryId = cc.Id";
            categoryWhereCondition = "WHERE Id = @CategoryId";
        }

        if (!string.IsNullOrWhiteSpace(@params.CategorySlug))
        {
            if (string.IsNullOrWhiteSpace(categoryWhereCondition))
            {
                joinWithCategories = @"
                    JOIN category_children cc
                        ON p.CategoryId = cc.Id";
                categoryWhereCondition = "WHERE Slug = @CategorySlug";
            }
            else
            {
                categoryWhereCondition += "AND Slug = @CategorySlug";
            }
        }

        if (!string.IsNullOrWhiteSpace(@params.Name))
            conditions += $" AND (p.Name LIKE N'%{@params.Name}%' OR p.EnglishName LIKE N'%{@params.Name}%')";

        if (@params.AverageScore != null)
            conditions += $" AND c.Score >= {@params.AverageScore}";

        if (@params.MinPrice != null)
            conditions += $" AND ({totalPrice} >= {@params.MinPrice} OR p.Price IS NULL)";

        if (@params.MaxPrice != null)
            conditions += $" AND ({totalPrice} <= {@params.MaxPrice} OR p.Price IS NULL)";

        if (@params.MinDiscountPercentage != null)
            conditions += $" AND (i.DiscountPercentage >= {@params.MinDiscountPercentage} " +
                          "OR i.DiscountPercentage IS NULL)";

        if (@params.MaxDiscountPercentage != null)
            conditions += $" AND (i.DiscountPercentage <= {@params.MaxDiscountPercentage} " +
                          "OR i.DiscountPercentage IS NULL)";

        if (@params.OnlyAvailable == true)
            conditions += " AND i.IsAvailable = 'True'";

        if (@params.OnlyDiscounted == true)
            conditions += " AND i.IsDiscounted = 'True'";

        if (@params.Attributes != null && @params.Attributes.Any())
        {
            joinWithCategorySpecifications = $@"
                LEFT JOIN {_dapperContext.ProductCategorySpecifications} pcs 
                    ON pcs.ProductId = p.Id
                LEFT JOIN {_dapperContext.CategorySpecifications} cs 
                    ON cs.Id = pcs.CategorySpecificationId";

            conditions += " AND (";
            @params.Attributes.ToList().ForEach(attr =>
            {
                var lastAttr = @params.Attributes.ToList().Last();
                var end = attr.Equals(lastAttr) ? ")" : " OR ";
                conditions += @$"pcs.Description LIKE '%{attr}%'{end}";
            });
        }

        var orderBy = @params.OrderBy switch
        {
            OrderBy.Cheapest => "ORDER BY Price",
            OrderBy.MostExpensive => "ORDER BY Price DESC",
            OrderBy.Latest => "ORDER BY p.Id DESC",
            _ => "ORDER BY AverageScore DESC"
        };

        using var connection = _dapperContext.CreateConnection();
        var sql = $@"
            WITH category_children AS (
                SELECT Id, ParentId, Title, Slug
                FROM {_dapperContext.Categories}
                {categoryWhereCondition}
                UNION ALL
                SELECT c.Id, c.ParentId, c.Title, c.Slug
                FROM {_dapperContext.Categories} c
                JOIN category_children cc
                    ON cc.Id = c.ParentId
            )
            SELECT DISTINCT
            	p.Id, p.Name, p.EnglishName, p.Slug, p.MainImage, p.CreationDate,
            	AVG(c.Score) OVER (PARTITION BY p.Id) AS AverageScore,
                i.Id AS InventoryId, MIN(i.Price) OVER (PARTITION BY p.Id) AS Price, i.DiscountPercentage,
            	q.Quantity AS AllQuantityInStock,
                clr.Id, clr.Name, clr.Code, clr.CreationDate
            FROM (
            	SELECT DISTINCT
                    p.Id, p.Name, p.EnglishName, p.Slug, p.MainImage, p.CreationDate, p.CategoryId,
					MIN(i.Price) OVER (PARTITION BY p.Id) AS Price,
                    AVG(c.Score) OVER (PARTITION BY p.Id) AS AverageScore
            	FROM {_dapperContext.Products} p
                {joinWithCategories}
                LEFT JOIN {_dapperContext.Comments} c
                	ON p.Id = c.ProductId
                LEFT JOIN {_dapperContext.SellerInventories} i
                	ON p.Id = i.ProductId
                {orderBy}
            	OFFSET @skip ROWS
            	FETCH NEXT @take ROWS ONLY
            ) AS p
            {joinWithCategories}
            LEFT JOIN {_dapperContext.Comments} c
            	ON p.Id = c.ProductId
            LEFT JOIN {_dapperContext.SellerInventories} i
            	ON p.Id = i.ProductId
            LEFT JOIN {_dapperContext.Colors} clr
            	ON clr.Id = i.ColorId
            LEFT JOIN (
            	SELECT ProductId, SUM(Quantity) AS Quantity
            	FROM {_dapperContext.SellerInventories}
            	GROUP BY ProductId
            ) AS q
            	ON p.Id = q.ProductId
            {joinWithCategorySpecifications}
            WHERE 1 = 1 {conditions}
            {orderBy}";

        var query = await connection
            .QueryAsync<ProductForShopDto, Color, ProductForShopDto>(sql, (productFilterDto, color) =>
            {
                if (color != null)
                    productFilterDto.Colors.Add(color);
                return productFilterDto;
            }, param: new { skip, take = @params.Take, @params.CategoryId, @params.CategorySlug }, splitOn: "Id");

        var groupedQueryResult = query.GroupBy(p => p.Id).Select(productGroup =>
        {
            var firstItem = productGroup.OrderBy(p => p.TotalPrice).First();
            if ((@params.OnlyDiscounted != null && (bool)@params.OnlyDiscounted)
                || @params.MinDiscountPercentage is > 0)
                firstItem = productGroup.OrderBy(p => p.Price).First();

            var colorList = productGroup.Select(p => p.Colors.FirstOrDefault())
                .Where(c => c != null).DistinctBy(c => c.Id).OrderBy(c => c.Id).ToList();
            firstItem.Colors = colorList;
            firstItem.AllQuantityInStock = productGroup.First().AllQuantityInStock;
            firstItem.AverageScore = productGroup.First().AverageScore;

            return firstItem;
        }).ToList();

        groupedQueryResult = @params.OrderBy switch
        {
            OrderBy.Cheapest => groupedQueryResult.OrderBy(p => p.TotalPrice).ToList(),
            OrderBy.MostExpensive => groupedQueryResult.OrderByDescending(p => p.TotalPrice).ToList(),
            OrderBy.Latest => groupedQueryResult.OrderByDescending(p => p.Id).ToList(),
            OrderBy.MostPopular => groupedQueryResult.OrderByDescending(p => p.AverageScore).ToList(),
            _ => groupedQueryResult.OrderByDescending(p => p.AverageScore).ToList()
        };

        groupedQueryResult = MoveOutOfStocksToLast(groupedQueryResult);

        var countSql = $@"
            WITH category_children AS (
               SELECT Id, ParentId, Title, Slug
               FROM {_dapperContext.Categories}
               {categoryWhereCondition}
               UNION ALL
               SELECT c.Id, c.ParentId, c.Title, c.Slug
               FROM {_dapperContext.Categories} c
               JOIN category_children cc
            		ON cc.Id = c.ParentId
            )
            SELECT COUNT(A.Id)
            FROM (
            	SELECT DISTINCT
            		p.Id, p.Name, p.EnglishName, p.Slug, p.MainImage, p.CreationDate,
            		ROW_NUMBER() OVER (PARTITION BY p.Id ORDER BY p.Id) AS RN,
            		AVG(c.Score) OVER (PARTITION BY p.Id) AS AverageScore,
            	    i.Id AS InventoryId, MIN(i.Price) OVER (PARTITION BY p.Id) AS Price, i.DiscountPercentage,
            		q.Quantity AS AllQuantityInStock,
            	    clr.Id AS ColorId, clr.Name AS ColorName, clr.Code AS ColorCode,
                    clr.CreationDate ColorCreationDate
                FROM (
            	    SELECT DISTINCT
                        p.Id, p.Name, p.EnglishName, p.Slug, p.MainImage, p.CreationDate, p.CategoryId,
				    	MIN(i.Price) OVER (PARTITION BY p.Id) AS Price,
                        AVG(c.Score) OVER (PARTITION BY p.Id) AS AverageScore
            	    FROM {_dapperContext.Products} p
                    {joinWithCategories}
                    LEFT JOIN {_dapperContext.Comments} c
                    	ON p.Id = c.ProductId
                    LEFT JOIN {_dapperContext.SellerInventories} i
                    	ON p.Id = i.ProductId
                ) AS p
            	{joinWithCategories}
            	LEFT JOIN {_dapperContext.Comments} c
            		ON p.Id = c.ProductId
            	LEFT JOIN {_dapperContext.SellerInventories} i
            		ON p.Id = i.ProductId
            	LEFT JOIN {_dapperContext.Colors} clr
            		ON clr.Id = i.ColorId
            	LEFT JOIN (
            		SELECT ProductId, SUM(Quantity) AS Quantity
            		FROM {_dapperContext.SellerInventories}
            		GROUP BY ProductId
            	) AS q
            		ON p.Id = q.ProductId 
                {joinWithCategorySpecifications}
                WHERE 1 = 1 {conditions}
            ) AS A
            WHERE 1 = 1 AND A.RN = 1";

        var count = await connection.QueryFirstAsync<int>(countSql,
            new { skip, take = @params.Take, @params.CategoryId, @params.CategorySlug });

        var model = new ProductForShopResult
        {
            Data = groupedQueryResult,
            FilterParams = @params,
            HighestPriceInCategory = highestPriceInCategory,
            Attributes = await GetCategoryAndParentsSpecificationsAndValues(@params.CategorySlug)
        };
        model.GeneratePaging(count, @params.Take, @params.PageId);
        return model;
    }

    private List<ProductForShopDto> MoveOutOfStocksToLast(List<ProductForShopDto> dtos)
    {
        var inStocks = new List<ProductForShopDto>();
        var outOfStocks = new List<ProductForShopDto>();
        var result = new List<ProductForShopDto>();

        dtos.ForEach(dto =>
        {
            if (dto.InventoryId == 0 || dto.AllQuantityInStock == 0)
                outOfStocks.Add(dto);
            else
                inStocks.Add(dto);
        });

        result.AddRange(inStocks);
        result.AddRange(outOfStocks);
        return result;
    }

    private async Task<List<ProductForShopAttributesDto>> GetCategoryAndParentsSpecificationsAndValues
        (string? categorySlug)
    {
        if (categorySlug == null)
            return new List<ProductForShopAttributesDto>();

        using var connection = _dapperContext.CreateConnection();
        var sql = $@"
            WITH parent_specs AS (
            	SELECT c.Id AS cId, c.ParentId, cs.*
            	FROM {_dapperContext.Categories} c
            	LEFT JOIN {_dapperContext.CategorySpecifications} cs
            		ON cs.CategoryId = c.Id
            	WHERE c.Slug = @categorySlug

            	UNION ALL
            	SELECT c.Id, c.ParentId, cs.*
            	FROM {_dapperContext.Categories} c
            	JOIN parent_specs p
            		ON p.ParentId = c.Id
            	JOIN {_dapperContext.CategorySpecifications} cs
            		ON cs.CategoryId = c.Id

                UNION ALL
            	SELECT c.Id, c.ParentId, cs.*
            	FROM {_dapperContext.Categories} c
            	JOIN parent_specs p
            		ON p.cId = c.ParentId
            	JOIN {_dapperContext.CategorySpecifications} cs
            		ON cs.CategoryId = c.Id
            )
            
            SELECT ps.*, pcs.Description
            INTO #TempTable
            FROM parent_specs ps
            JOIN product.CategorySpecifications pcs
            	ON ps.Id = pcs.CategorySpecificationId
            ALTER TABLE #TempTable DROP COLUMN ParentId
            SELECT * FROM #TempTable tmp
            WHERE tmp.Id IS NOT NULL
            ORDER BY tmp.Id ASC
            DROP TABLE #TempTable";

        var attributes = new List<ProductForShopAttributesDto>();
        await connection.QueryAsync<CategorySpecification, string, CategorySpecification>(sql, (spec, specValue) =>
        {
            var attribute = new ProductForShopAttributesDto
            {
                Id = spec.Id,
                CreationDate = spec.CreationDate,
                Title = spec.Title
            };
            attribute.Values.Add(specValue);
            attributes.Add(attribute);
            return spec;
        }, param: new { categorySlug }, splitOn: "Description");

        var groupedAttributes = attributes.GroupBy(a => a.Id).Select(group =>
        {
            var firstAttr = group.First();
            firstAttr.Values = group.Select(a => a.Values.First()).Distinct().ToList();
            return firstAttr;
        }).ToList();

        return groupedAttributes;
    }
}