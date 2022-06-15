namespace Shop.UI.Models.Orders;

public class SetOrderStatusCommandViewModel
{
    public long UserId { get; set; }
    public string OrderStatus { get; set; }
}