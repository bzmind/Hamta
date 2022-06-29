namespace Shop.API.CommandViewModels.Orders;

public class IncreaseOrderItemCountCommandViewModel
{
    public long InventoryId { get; init; }
    public long OrderItemId { get; init; }
}