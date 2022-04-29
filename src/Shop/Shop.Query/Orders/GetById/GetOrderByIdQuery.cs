using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Orders._DTOs;
using Shop.Query.Orders._Mappers;

namespace Shop.Query.Orders.GetById;

public record GetOrderByIdQuery(long OrderId) : IBaseQuery<OrderDto?>;

public class GetOrderByIdQueryHandler : IBaseQueryHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly ShopContext _shopContext;
    private readonly DapperContext _dapperContext;

    public GetOrderByIdQueryHandler(ShopContext shopContext, DapperContext dapperContext)
    {
        _shopContext = shopContext;
        _dapperContext = dapperContext;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _shopContext.Orders.FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
        var orderDto = order.MapToOrderDto();
        orderDto.Items = await orderDto.GetOrderItemsAsDto(_dapperContext);
        return orderDto;
    }
}