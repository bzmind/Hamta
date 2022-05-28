﻿using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Orders._DTOs;
using Shop.Query.Orders._Mappers;

namespace Shop.Query.Orders.GetByFilter;

public class GetOrderByFilterQuery : BaseFilterQuery<OrderFilterResult, OrderFilterParam>
{
    public GetOrderByFilterQuery(OrderFilterParam filterParams) : base(filterParams)
    {
    }
}

public class GetOrderByFilterQueryHandler : IBaseQueryHandler<GetOrderByFilterQuery, OrderFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetOrderByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<OrderFilterResult> Handle(GetOrderByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var query = _shopContext.Orders
            .OrderByDescending(o => o.CreationDate)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                CreationDate = o.CreationDate,
                CustomerId = o.CustomerId,
                Status = o.Status,
                Address = o.Address.MapToOrderAddressDto(),
                ShippingMethod = o.ShippingInfo.ShippingMethod,
                ShippingCost = o.ShippingInfo.ShippingCost.Value,
                Items = o.Items.ToList().MapToOrderItemDto()
            }).AsQueryable();

        if (@params.CustomerId != null)
            query = query.Where(o => o.CustomerId == @params.CustomerId);

        if (@params.StartDate != null)
            query = query.Where(o => o.CreationDate >= @params.StartDate);

        if (@params.EndDate != null)
            query = query.Where(o => o.CreationDate <= @params.EndDate);

        if (@params.Status != null)
            query = query.Where(o => o.Status == @params.Status.ToString());

        var skip = (@params.PageId - 1) * @params.Take;

        var finalQuery = await query
            .Skip(skip)
            .Take(@params.Take)
            .ToListAsync(cancellationToken);

        var itemInventoryIds = new List<long>();

        finalQuery.ForEach(orderDto =>
        {
            orderDto.Items.ForEach(orderItem =>
            {
                itemInventoryIds.Add(orderItem.InventoryId);
            });
        });

        var inventoryDetails = await _shopContext.Inventories
            .Where(i => itemInventoryIds.Contains(i.Id))
            .Join(
                _shopContext.Colors,
                i => i.ColorId,
                c => c.Id,
                (inventory, color) => new
                {
                    inventory,
                    color
                })
            .Join(
                _shopContext.Products,
                t => t.inventory.ProductId,
                p => p.Id,
                (tables, product) => new
                {
                    tables.inventory,
                    tables.color,
                    product
                })
            .ToListAsync(cancellationToken);

        finalQuery.ForEach(orderDto =>
        {
            orderDto.Items.ForEach(orderItemDto =>
            {
                var item = inventoryDetails.First(t => t.inventory.Id == orderItemDto.InventoryId);
                orderItemDto.ProductName = item.product.Name;
                orderItemDto.InventoryQuantity = item.inventory.Quantity;
                orderItemDto.ColorName = item.color.Name;
                orderItemDto.ColorCode = item.color.Code;
            });
        });
        
        return new OrderFilterResult
        {
            Data = finalQuery,
            FilterParam = @params
        };
    }
}