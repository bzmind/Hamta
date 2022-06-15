namespace Shop.UI.Models.Inventories;

public class EditInventoryCommandViewModel
{
    public int InventoryId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public int ColorId { get; set; }
}