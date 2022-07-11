namespace Shop.API.ViewModels.Inventories;

public class CreateInventoryViewModel
{
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public long ColorId { get; set; }
}