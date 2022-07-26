using Common.Query.BaseClasses.FilterQuery;

namespace Shop.Query.Sellers._DTOs;

public class SellerInventoryFilterResult : BaseFilterResult<SellerInventoryDto, SellerInventoryFilterParams>
{
    
}

public class SellerInventoryFilterParams : BaseFilterParams
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