namespace Shop.API.ViewModels.Orders;

public class CheckoutOrderCommandViewModel
{
    public long UserAddressId { get; init; }
    public long ShippingMethodId { get; init; }
}