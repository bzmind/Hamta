using Common.Query.BaseClasses;
using Dapper;
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
                        p.Name,
                        i.Price,
                        AVG(p.Scores),
                        i.Quantity,
                        c.Code
                    FROM {_dapperContext.Products} p
                    INNER JOIN {_dapperContext.Inventories} i
                        ON i.ProductId = p.Id
                    INNER JOIN {_dapperContext.Colors} c
                        ON i.ColorId = c.Id";

        var result = await connection.QueryAsync<ProductListDto, string, ProductListDto>(sql,
            (productListDto, colorCode) =>
            {
                productListDto.ColorCodes.Add(colorCode);
                return productListDto;
            }, splitOn: "Code");

        return result.ToList();
    }
}