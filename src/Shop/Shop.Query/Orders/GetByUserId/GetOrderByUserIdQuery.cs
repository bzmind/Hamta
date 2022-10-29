using Common.Domain.ValueObjects;
using Common.Query.BaseClasses;
using Dapper;
using Shop.Infrastructure;
using Shop.Query.Colors._DTOs;
using Shop.Query.Orders._DTOs;

namespace Shop.Query.Orders.GetByUserId;

public record GetOrderByUserIdQuery(long UserId) : IBaseQuery<OrderDto?>;

public class GetOrderByUserIdQueryHandler : IBaseQueryHandler<GetOrderByUserIdQuery, OrderDto?>
{
    private readonly DapperContext _dapperContext;

    public GetOrderByUserIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<OrderDto?> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();

        var sql = $@"
            SELECT
                o.Id, o.CreationDate, o.UserId, o.Status, o.ShippingName, o.ShippingCost,
                oa.Id, oa.CreationDate, oa.OrderId, oa.FullName, oa.Province, oa.City,
                oa.FullAddress, oa.PostalCode, oa.PhoneNumber AS Value, oi.Id, oi.CreationDate,
                oi.OrderId, oi.InventoryId, p.Name AS ProductName, p.MainImage AS ProductMainImage,
                p.Slug AS ProductSlug, oi.Count, oi.Price, i.DiscountPercentage AS InventoryDiscountPercentage,
                s.ShopName AS InventoryShopName, i.Quantity AS InventoryQuantity,
                c.Id, c.CreationDate, c.Name, c.Code
            FROM {_dapperContext.Orders} o
            LEFT JOIN {_dapperContext.OrderAddresses} oa
                ON oa.OrderId = o.Id
            LEFT JOIN {_dapperContext.OrderItems} oi
                ON oi.OrderId = o.Id
            LEFT JOIN {_dapperContext.SellerInventories} i
                ON oi.InventoryId = i.Id
            LEFT JOIN {_dapperContext.Colors} c
                ON i.ColorId = c.Id
            LEFT JOIN {_dapperContext.Products} p
                ON i.ProductId = p.Id
            LEFT JOIN {_dapperContext.Sellers} s
                ON s.Id = i.SellerId
            WHERE o.UserId = @UserId AND o.Status = 'Pending'";

        var result = await connection.QueryAsync<OrderDto, OrderAddressDto, PhoneNumber, OrderItemDto,
            ColorDto, OrderDto>
        (sql, (orderDto, orderAddressDto, phoneNumber, itemDto, colorDto) =>
        {
            if (orderAddressDto != null)
            {
                orderDto.Address = orderAddressDto;
                orderDto.Address.PhoneNumber = phoneNumber;
            }
            itemDto.ColorName = colorDto.Name;
            itemDto.ColorCode = colorDto.Code;
            orderDto.Items.Add(itemDto);
            return orderDto;
        }, splitOn: "Id,Value,Id,Id", param: new { request.UserId });

        var groupedResult = result.GroupBy(o => o.Id).Select(orderGroup =>
        {
            var firstItem = orderGroup.First();
            firstItem.Items = orderGroup.Select(o => o.Items.Single()).ToList();
            return firstItem;
        }).SingleOrDefault();

        return groupedResult;
    }
}