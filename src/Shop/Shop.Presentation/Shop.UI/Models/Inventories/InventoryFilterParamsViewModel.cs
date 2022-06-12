using Shop.UI.Models._Filters;

namespace Shop.UI.Models.Inventories;

public class InventoryFilterParamsViewModel : BaseFilterParamsViewModel
{
    public long? ProductId { get; set; }
    public int? StartQuantity { get; set; }
    public int? EndQuantity { get; set; }
    public int? StartPrice { get; set; }
    public int? EndPrice { get; set; }
    public int? StartDiscountPercentage { get; set; }
    public int? EndDiscountPercentage { get; set; }
    public bool? IsAvailable { get; set; }
    public bool? IsDiscounted { get; set; }
}