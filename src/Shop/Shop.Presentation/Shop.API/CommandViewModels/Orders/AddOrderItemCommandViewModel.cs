namespace Shop.API.CommandViewModels.Orders;

public class AddOrderItemCommandViewModel
{
    public long InventoryId { get; init; }
    public int Quantity { get; init; }
}