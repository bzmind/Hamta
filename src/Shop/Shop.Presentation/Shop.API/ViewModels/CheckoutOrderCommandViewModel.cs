namespace Shop.API.ViewModels;

public record CheckoutOrderCommandViewModel(long UserAddressId, long ShippingMethodId);