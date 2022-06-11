using Common.Query.BaseClasses.FilterQuery;

namespace Shop.Query.Inventories._DTOs;

public class InventoryFilterResult : BaseFilterResult<InventoryDto, InventoryFilterParams>
{
    
}

public class InventoryFilterParams : BaseFilterParams
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