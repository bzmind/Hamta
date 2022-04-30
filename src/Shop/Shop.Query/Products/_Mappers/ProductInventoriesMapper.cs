using Dapper;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductInventoriesMapper
{
    public static async Task<List<ProductInventoryDto>> GetProductInventoriesAsDto(this ProductDto? productDto,
        DapperContext dapperContext)
    {
        if (productDto == null)
            return null;

        using var connection = dapperContext.CreateConnection();
        var sql = $@"SELECT
                        i.ProductId, i.Quantity, i.Price, c.Name AS ColorName, c.Code AS ColorCode,
                        i.IsAvailable, i.DiscountPercentage, i.IsDiscounted
                    FROM {dapperContext.Inventories} i
                    INNER JOIN {dapperContext.Colors} c
                        ON i.ColorId == c.Id
                    WHERE i.ProductId == @ProductDtoId";

        var result = await connection.QueryAsync<ProductInventoryDto>(sql, new { ProductDtoId = productDto.Id });
        return result.ToList();
    }
}