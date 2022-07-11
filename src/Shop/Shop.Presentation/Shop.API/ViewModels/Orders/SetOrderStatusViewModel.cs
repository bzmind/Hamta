using Shop.Domain.OrderAggregate;

namespace Shop.API.ViewModels.Orders;

public class SetOrderStatusViewModel
{
    public long UserId { get; set; }
    public Order.OrderStatus Status { get; set; }
}