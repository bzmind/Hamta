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
                        fi.CustomerId, fi.ProductId, p.Name AS ProductName, pi.Name AS ProductMainImage,
                        i.Price AS ProductPrice, AVG(ps.Value) AS AverageScore, i.IsAvailable
                    FROM {dapperContext.CustomerFavoriteItems} fi
                    LEFT JOIN {dapperContext.Products} p
                        ON p.id = fi.ProductId
                    LEFT JOIN {dapperContext.ProductImages} pi
                        ON pi.ProductId = p.Id
                    LEFT JOIN {dapperContext.ProductScores} ps
                        ON ps.ProductId = p.Id
                    LEFT JOIN {dapperContext.Inventories} i
                        ON i.ProductId = fi.ProductId
                    WHERE fi.CustomerId = @CustomerDtoId
                    GROUP BY
                        fi.CustomerId, fi.ProductId, p.Name, pi.Name, i.Price, i.IsAvailable";

        var result = await connection
            .QueryAsync<CustomerFavoriteItemDto>(sql, new { CustomerDtoId = customerDto.Id });

        return result.ToList();
    }
}