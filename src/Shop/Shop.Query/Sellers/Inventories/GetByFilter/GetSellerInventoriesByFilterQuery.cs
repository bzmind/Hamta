using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Sellers._DTOs;
using Shop.Query.Sellers._Mappers;

namespace Shop.Query.Sellers.Inventories.GetByFilter;

public class GetSellerInventoriesByFilterQuery :
    BaseFilterQuery<SellerInventoryFilterResult, SellerInventoryFilterParams>
{
    public GetSellerInventoriesByFilterQuery(SellerInventoryFilterParams filterParams) : base(filterParams)
    {
    }
}

public class GetSellerInventoriesByFilterQueryHandler :
    IBaseQueryHandler<GetSellerInventoriesByFilterQuery, SellerInventoryFilterResult>
{
    private readonly ShopContext _shopContext;
    private readonly DapperContext _dapperContext;

    public GetSellerInventoriesByFilterQueryHandler(ShopContext shopContext, DapperContext dapperContext)
    {
        _shopContext = shopContext;
        _dapperContext = dapperContext;
    }

    public async Task<SellerInventoryFilterResult> Handle(GetSellerInventoriesByFilterQuery request,
        CancellationToken cancellationToken)
    {
        var @params = request.FilterFilterParams;
        var skip = (@params.PageId - 1) * @params.Take;

        var conditions = "";

        if (@params.ProductName != null && !string.IsNullOrWhiteSpace(@params.ProductName))
            conditions += $" AND (p.Name N'%{@params.ProductName}%' OR p.EnglishName LIKE N'%{@params.ProductName}%')";

        if (@params.MinQuantity != null)
            conditions += $" AND (si.Quantity >= {@params.MinQuantity})";

        if (@params.MaxQuantity != null)
            conditions += $" AND (si.Quantity <= {@params.MaxQuantity})";

        if (@params.MinPrice != null)
            conditions += $" AND (si.Price >= {@params.MinPrice})";

        if (@params.MaxPrice != null)
            conditions += $" AND (si.Price <= {@params.MaxPrice})";

        if (@params.MinDiscountPercentage != null)
            conditions += $" AND (si.DiscountPercentage >= {@params.MinDiscountPercentage})";

        if (@params.MaxDiscountPercentage != null)
            conditions += $" AND (si.DiscountPercentage <= {@params.MaxDiscountPercentage})";

        if (@params.OnlyAvailable == true)
            conditions += " AND (si.IsAvailable = 1)";

        if (@params.OnlyDiscounted == true)
            conditions += " AND (si.IsDiscounted = 1)";

        using var connection = _dapperContext.CreateConnection();
        var sql = $@"
            SELECT
            	si.Id, si.CreationDate, si.Price, si.Quantity, si.IsAvailable,
                si.DiscountPercentage, si.IsDiscounted,
            	s.ShopName,
            	p.Id AS ProductId, p.Name AS ProductName, p.EnglishName AS ProductEnglishName,
                p.MainImage AS ProductMainImage,
            	c.Id AS ColorId, c.Name AS ColorName, c.Code AS ColorCode
            FROM {_dapperContext.Sellers} s
            JOIN {_dapperContext.SellerInventories} si
            	ON si.SellerId = s.Id
            JOIN {_dapperContext.Products} p
            	ON p.Id = si.ProductId
            JOIN {_dapperContext.Colors} c
            	ON si.ColorId = c.Id
            WHERE s.UserId = @UserId {conditions}
            ORDER BY si.CreationDate DESC
            OFFSET @skip ROWS
            FETCH NEXT @take ROWS ONLY";

        var result = await connection.QueryAsync<SellerInventoryDto>(sql, new
        {
            skip,
            take = @params.Take,
            @params.UserId
        });

        var highestPriceSql = $@"
            SELECT TOP(1) si.Price AS HighestPrice
            FROM {_dapperContext.SellerInventories} si
            JOIN {_dapperContext.Sellers} s
            	ON s.Id = si.SellerId
            WHERE s.UserId = @UserId
            ORDER BY (si.Price - si.Price * si.DiscountPercentage / 100) DESC";

        var highestPrice = await connection.QueryFirstAsync<int>(highestPriceSql, new { @params.UserId });

        var countSql = $@"
            SELECT COUNT(si.Id)
            FROM {_dapperContext.Sellers} s
            JOIN {_dapperContext.SellerInventories} si
            	ON si.SellerId = s.Id
            JOIN {_dapperContext.Products} p
            	ON p.Id = si.ProductId
            JOIN {_dapperContext.Colors} c
            	ON si.ColorId = c.Id
            WHERE s.UserId = @UserId";

        var count = await connection.QueryFirstAsync<int>(countSql, new { @params.UserId });

        var model = new SellerInventoryFilterResult
        {
            Data = result.ToList(),
            FilterParams = @params,
            HighestPriceInInventory = highestPrice
        };
        model.GeneratePaging(count, @params.Take, @params.PageId);
        return model;
    }
}