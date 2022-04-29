using Dapper;
using Shop.Query.Customers._DTOs;

namespace Shop.Query.Customers._Mappers;

internal static class CustomerFavoriteItemMapper
{
    public static async Task<List<CustomerFavoriteItemDto>> GetFavoriteItemsAsDto(this CustomerDto? customerDto,
        DapperContext dapperContext)
    {
        if (customerDto == null)
            return null;

        using var connection = dapperContext.CreateConnection();
        var sql = $@"SELECT
                        fi.CustomerId,
                        fi.ProductId,
                        p.Name AS ProductName,
                        p.MainImage AS ProductMainImage,
                        i.Price AS ProductPrice,
                        AVG(p.Scores) AS AverageScore,
                        i.IsAvailable
                    FROM {dapperContext.CustomerFavoriteItems} fi
                    WHERE fi.CustomerId == @CustomerDtoId
                    INNER JOIN {dapperContext.Products} p
                        ON fi.ProductId == p.Id
                    INNER JOIN {dapperContext.Inventories} i
                        ON fi.ProductId == i.ProductId";

        var result = await connection
            .QueryAsync<CustomerFavoriteItemDto>(sql, new { CustomerDtoId = customerDto.Id });

        return result.ToList();
    }
}