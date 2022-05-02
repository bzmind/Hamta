using Common.Query.BaseClasses;
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
            .Join(
                _shopContext.Inventories,
                o => o.Items.First().InventoryId, // This might not work! all line which use First() on the items, may not work.
                i => i.Id,
                (order, inventory) => new
                {
                    Order = order,
                    Inventory = inventory
                })
            .Join(
                _shopContext.Colors,
                tables => tables.Inventory.ColorId,
                c => c.Id,
                (orderAndInventory, color) => new
                {
                    Order = orderAndInventory.Order,
                    Inventory = orderAndInventory.Inventory,
                    Color = color
                })
            .Join(
                _shopContext.Products,
                tables => tables.Inventory.ProductId,
                p => p.Id,
                (tables, product) => new OrderDto
                {
                    Id = tables.Order.Id,
                    CreationDate = tables.Order.CreationDate,
                    CustomerId = tables.Order.CustomerId,
                    Status = tables.Order.Status,
                    Address = tables.Order.Address.MapToOrderAddressDto(),
                    ShippingInfo = tables.Order.ShippingInfo,
                    Items = new List<OrderItemDto>
                    {
                        new OrderItemDto
                        {
                            Id = tables.Order.Items.First().Id,
                            CreationDate = tables.Order.Items.First().CreationDate,
                            OrderId = tables.Order.Id,
                            InventoryId = tables.Inventory.Id,
                            ProductName = product.Name,
                            Count = tables.Order.Items.First().Count,
                            Price = tables.Order.Items.First().Price.Value,
                            InventoryQuantity = tables.Inventory.Quantity,
                            ColorName = tables.Color.Name,
                            ColorCode = tables.Color.Code
                        }
                    }
                })
            .AsQueryable();

        if (@params.CustomerId != null)
            query = query.Where(o => o.CustomerId == @params.CustomerId);

        if (@params.StartDate != null)
            query = query.Where(o => o.CreationDate >= @params.StartDate);

        if (@params.EndDate != null)
            query = query.Where(o => o.CreationDate <= @params.EndDate);

        if (@params.Status != null)
            query = query.Where(o => o.Status == @params.Status);

        var skip = (@params.PageId - 1) * @params.Take;

        var finalQuery = await query
            .Skip(skip)
            .Take(@params.Take)
            .ToListAsync(cancellationToken);

        var groupedResult = finalQuery.GroupBy(o => o.Id).Select(orderGroup =>
        {
            var firstItem = orderGroup.First();
            firstItem.Items = orderGroup.Select(o => o.Items.First()).ToList();
            return firstItem;
        }).ToList();

        return new OrderFilterResult
        {
            Data = groupedResult,
            FilterParam = @params
        };
    }
}