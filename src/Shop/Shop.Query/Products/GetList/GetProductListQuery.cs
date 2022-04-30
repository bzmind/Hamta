using Common.Query.BaseClasses;
using Dapper;
using Shop.Domain.ColorAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products.GetList;

public record GetProductListQuery : IBaseQuery<List<ProductListDto>>;

public class GetProductListQueryHandler : IBaseQueryHandler<GetProductListQuery, List<ProductListDto>>
{
    private readonly DapperContext _dapperContext;

    public GetProductListQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<List<ProductListDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $@"SELECT
                        p.Id, p.CreationDate, p.CategoryId, p.Name, p.EnglishName, p.Slug, i.Price,
                        AVG(p.Scores) AS AverageScore, i.Quantity, c.Name, c.Code
                    FROM {_dapperContext.Products} p
                    INNER JOIN {_dapperContext.Inventories} i
                        ON i.ProductId = p.Id
                    INNER JOIN {_dapperContext.Colors} c
                        ON i.ColorId = c.Id";

        var result = await connection.QueryAsync<ProductListDto, Color, ProductListDto>(sql,
            (productListDto, colorCode) =>
            {
                productListDto.Colors.Add(colorCode);
                return productListDto;
            }, splitOn: "Name");

        return result.ToList();
    }
}