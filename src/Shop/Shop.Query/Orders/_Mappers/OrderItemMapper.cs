using Dapper;
using Shop.Query.Orders._DTOs;

namespace Shop.Query.Orders._Mappers;

internal static class OrderItemMapper
{
    public static async Task<List<OrderItemDto>> GetOrderItemsAsDto(this OrderDto? orderDto,
        DapperContext dapperContext)
    {
        if (orderDto == null)
            return null;

        using var connection = dapperContext.CreateConnection();
        var sql = $@"SELECT
                        oi.OrderId,
                        oi.InventoryId,
                        oi.Count,
                        oi.Price,
                        i.Quantity AS InventoryQuantity,
                        p.Name AS ProductName,
                        c.Name AS ColorName,
                        c.Code AS ColorCode,
                    FROM {dapperContext.OrderItems} oi
                    WHERE oi.OrderId == @OrderDtoId
                    INNER JOIN {dapperContext.Inventories} i
                        ON oi.InventoryId == i.Id
                    INNER JOIN {dapperContext.Colors} c
                        ON i.ColorId == c.Id
                    INNER JOIN {dapperContext.Products} p
                        ON oi.ProductId == p.Id";

        var result = await connection.QueryAsync<OrderItemDto>(sql, new { OrderDtoId = orderDto.Id });
        return result.ToList();
    }
}