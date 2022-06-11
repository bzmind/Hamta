namespace Shop.API.ViewModels.Orders;

public class IncreaseOrderItemCountCommandViewModel
{
    public long InventoryId { get; init; }
    public long OrderItemId { get; init; }
}