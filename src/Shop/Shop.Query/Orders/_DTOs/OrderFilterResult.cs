using Common.Query.BaseClasses.FilterQuery;
using Shop.Domain.OrderAggregate;

namespace Shop.Query.Orders._DTOs;

public class OrderFilterResult : BaseFilterResult<OrderDto, OrderFilterParams>
{
}

public class OrderFilterParams : BaseFilterParams
{
    public long? UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Order.OrderStatus? Status { get; set; }
}