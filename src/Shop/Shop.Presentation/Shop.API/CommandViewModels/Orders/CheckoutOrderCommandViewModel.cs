namespace Shop.API.CommandViewModels.Orders;

public class CheckoutOrderCommandViewModel
{
    public long UserAddressId { get; init; }
    public long ShippingMethodId { get; init; }
}