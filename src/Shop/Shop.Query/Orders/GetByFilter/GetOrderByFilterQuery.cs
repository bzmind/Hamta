using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Orders._DTOs;
using Shop.Query.Orders._Mappers;

namespace Shop.Query.Orders.GetByFilter;

public class GetOrderByFilterQuery : BaseFilterQuery<OrderFilterResult, OrderFilterParams>
{
    public GetOrderByFilterQuery(OrderFilterParams filterParams) : base(filterParams)
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
        var @params = request.FilterFilterParams;

        var query = _shopContext.Orders
            .OrderByDescending(o => o.CreationDate)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                CreationDate = o.CreationDate,
                UserId = o.UserId,
                Status = o.Status,
                Address = o.Address.MapToOrderAddressDto(),
                ShippingName = o.ShippingInfo.ShippingName,
                ShippingCost = o.ShippingInfo.ShippingCost.Value,
                Items = o.Items.ToList().MapToOrderItemDto()
            }).AsQueryable();

        if (@params.UserId != null)
            query = query.Where(o => o.UserId == @params.UserId);

        if (@params.StartDate != null)
            query = query.Where(o => o.CreationDate >= @params.StartDate);

        if (@params.EndDate != null)
            query = query.Where(o => o.CreationDate <= @params.EndDate);

        if (@params.Status != null)
            query = query.Where(o => o.Status == @params.Status);

        var skip = (@params.PageId - 1) * @params.Take;

        var queryResult = await query
            .Skip(skip)
            .Take(@params.Take)
            .ToListAsync(cancellationToken);

        var itemInventoryIds = new List<long>();

        queryResult.ForEach(orderDto =>
        {
            orderDto.Items.ForEach(orderItem =>
            {
                itemInventoryIds.Add(orderItem.InventoryId);
            });
        });

        var inventoryDetails = await _shopContext.Sellers
            .SelectMany(seller => seller.Inventories)
            .Where(i => itemInventoryIds.Contains(i.Id))
            .Join(
                _shopContext.Colors,
                i => i.ColorId,
                c => c.Id,
                (inventory, color) => new { inventory, color })
            .Join(
                _shopContext.Products,
                t => t.inventory.ProductId,
                p => p.Id,
                (tables, product) => new { tables.inventory, tables.color, product })
            .ToListAsync(cancellationToken);

        queryResult.ForEach(orderDto =>
        {
            orderDto.Items.ForEach(orderItemDto =>
            {
                var item = inventoryDetails.First(t => t.inventory.Id == orderItemDto.InventoryId);
                orderItemDto.ProductName = item.product.Name;
                orderItemDto.ProductMainImage = item.product.MainImage;
                orderItemDto.ProductSlug = item.product.Slug;
                orderItemDto.InventoryDiscountPercentage = item.inventory.DiscountPercentage;
                orderItemDto.InventoryQuantity = item.inventory.Quantity;
                orderItemDto.ColorName = item.color.Name;
                orderItemDto.ColorCode = item.color.Code;
            });
        });
        
        var model = new OrderFilterResult
        {
            Data = queryResult,
            FilterParams = @params
        };
        model.GeneratePaging(query.Count(), @params.Take, @params.PageId);
        return model;
    }
}