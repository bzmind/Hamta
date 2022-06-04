namespace Shop.API.ViewModels.Orders;

public record IncreaseOrderItemCountCommandViewModel(long InventoryId, long OrderItemId);