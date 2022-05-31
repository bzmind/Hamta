using Dapper;
using Shop.Query.Users._DTOs;

namespace Shop.Query.Users._Mappers;

internal static class UserFavoriteItemMapper
{
    public static async Task<List<UserFavoriteItemDto>> GetFavoriteItemsAsDto(this UserDto? userDto,
        DapperContext dapperContext)
    {
        if (userDto == null)
            return null;

        using var connection = dapperContext.CreateConnection();
        var sql = $@"SELECT
                        fi.UserId, fi.ProductId, p.Name AS ProductName, pi.Name AS ProductMainImage,
                        i.Price AS ProductPrice, AVG(ps.Value) AS AverageScore, i.IsAvailable
                    FROM {dapperContext.UserFavoriteItems} fi
                    LEFT JOIN {dapperContext.Products} p
                        ON p.id = fi.ProductId
                    LEFT JOIN {dapperContext.ProductImages} pi
                        ON pi.ProductId = p.Id
                    LEFT JOIN {dapperContext.ProductScores} ps
                        ON ps.ProductId = p.Id
                    LEFT JOIN {dapperContext.Inventories} i
                        ON i.ProductId = fi.ProductId
                    WHERE fi.UserId = @UserDtoId
                    GROUP BY
                        fi.UserId, fi.ProductId, p.Name, pi.Name, i.Price, i.IsAvailable";

        var result = await connection
            .QueryAsync<UserFavoriteItemDto>(sql, new { UserDtoId = userDto.Id });

        return result.ToList();
    }
}