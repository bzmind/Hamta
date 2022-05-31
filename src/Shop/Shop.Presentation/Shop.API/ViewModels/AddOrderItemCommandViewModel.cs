namespace Shop.API.ViewModels;

public record AddOrderItemCommandViewModel(long InventoryId, int Quantity);