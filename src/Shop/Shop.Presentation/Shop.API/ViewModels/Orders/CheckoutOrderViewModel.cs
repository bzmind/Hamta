namespace Shop.API.ViewModels.Orders;

public class CheckoutOrderViewModel
{
    public long UserAddressId { get; init; }
    public long ShippingMethodId { get; init; }
}