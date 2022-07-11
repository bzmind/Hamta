namespace Shop.API.ViewModels.Inventories;

public class EditInventoryViewModel
{
    public long InventoryId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public long ColorId { get; set; }
}