namespace Shop.API.ViewModels.Orders;

public record AddOrderItemCommandViewModel(long InventoryId, int Quantity);