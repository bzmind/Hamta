using Common.Query.BaseClasses;
using Dapper;
using Shop.Query.Orders._DTOs;

namespace Shop.Query.Orders.GetById;

public record GetOrderByIdQuery(long OrderId) : IBaseQuery<OrderDto?>;

public class GetOrderByIdQueryHandler : IBaseQueryHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly DapperContext _dapperContext;

    public GetOrderByIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $@"SELECT
                        o.*,
                        oi.OrderId,
                        oi.InventoryId,
                        p.Name AS ProductName,
                        oi.Count,
                        oi.Price,
                        i.Quantity AS InventoryQuantity,
                        c.Name AS ColorName,
                        c.Code AS ColorCode,
                    FROM {_dapperContext.Orders} o
                    WHERE o.Id = @OrderId
                    INNER JOIN {_dapperContext.OrderItems} oi
                        ON oi.OrderId == @OrderDtoId
                    INNER JOIN {_dapperContext.Inventories} i
                        ON oi.InventoryId == i.Id
                    INNER JOIN {_dapperContext.Colors} c
                        ON i.ColorId == c.Id
                    INNER JOIN {_dapperContext.Products} p
                        ON i.ProductId == p.Id";

        var result = await connection.QueryAsync<OrderDto, OrderItemDto, OrderDto>(sql, (orderDto, itemDto) =>
        {
            orderDto.Items.Add(itemDto);
            return orderDto;
        }, splitOn: "OrderId");

        var groupedResult = result.GroupBy(o => o.Id).Select(orderGroup =>
        {
            var firstItem = orderGroup.First();
            firstItem.Items = orderGroup.Select(o => o.Items.Single()).ToList();
            return firstItem;
        }).Single();
        
        return groupedResult;
    }
}