namespace Shop.API.ViewModels.Orders;

public record CheckoutOrderCommandViewModel(long UserAddressId, long ShippingMethodId);