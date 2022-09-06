using Common.Query.BaseClasses.FilterQuery;

namespace Shop.Query.Sellers._DTOs;

public class SellerInventoryFilterResult : BaseFilterResult<SellerInventoryDto, SellerInventoryFilterParams>
{
    
}

public class SellerInventoryFilterParams : BaseFilterParams
{
    public long UserId { get; set; }
    public long? ProductId { get; set; }
    public int? MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinDiscountPercentage { get; set; }
    public int? MaxDiscountPercentage { get; set; }
    public bool? IsAvailable { get; set; }
    public bool? IsDiscounted { get; set; }
}