using Common.Domain.ValueObjects;
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
                        o.Id, o.CreationDate, o.UserId, o.Status, o.ShippingName, o.ShippingCost,
                        oa.Id, oa.CreationDate, oa.OrderId, oa.FullName, oa.Province, oa.City,
                        oa.FullAddress, oa.PostalCode, oa.PhoneNumber, oi.Id, oi.CreationDate,
                        oi.OrderId, oi.InventoryId, p.Name AS ProductName, oi.Count, oi.Price,
                        i.Quantity AS InventoryQuantity, c.Name AS ColorName, c.Code AS ColorCode
                    FROM {_dapperContext.Orders} o
                    INNER JOIN {_dapperContext.OrderAddresses} oa
                        ON oa.OrderId = @OrderId
                    INNER JOIN {_dapperContext.OrderItems} oi
                        ON oi.OrderId = @OrderId
                    INNER JOIN {_dapperContext.Inventories} i
                        ON oi.InventoryId = i.Id
                    INNER JOIN {_dapperContext.Colors} c
                        ON i.ColorId = c.Id
                    INNER JOIN {_dapperContext.Products} p
                        ON i.ProductId = p.Id
                    WHERE o.Id = @OrderId";

        var result = await connection.QueryAsync<OrderDto, OrderAddressDto, PhoneNumber, OrderItemDto, OrderDto>
        (sql, (orderDto, orderAddressDto, phoneNumber, itemDto) =>
        {
            orderDto.Address = orderAddressDto;
            orderDto.Address.PhoneNumber = phoneNumber;
            orderDto.Items.Add(itemDto);
            return orderDto;
        }, splitOn: "Id,PhoneNumber,Id", param: new { request.OrderId });

        var groupedResult = result.GroupBy(o => o.Id).Select(orderGroup =>
        {
            var firstItem = orderGroup.First();
            firstItem.Items = orderGroup.Select(o => o.Items.Single()).ToList();
            return firstItem;
        }).Single();

        return groupedResult;
    }
}