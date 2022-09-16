using System.ComponentModel;
using Common.Query.BaseClasses.FilterQuery;

namespace Shop.Query.Sellers._DTOs;

public class SellerInventoryFilterResult : BaseFilterResult<SellerInventoryDto, SellerInventoryFilterParams>
{
    public int HighestPriceInInventory { get; set; }
}

public class SellerInventoryFilterParams : BaseFilterParams
{
    [DisplayName("کاربر")]
    public long UserId { get; set; }

    [DisplayName("نام محصول")]
    public string? ProductName { get; set; }

    [DisplayName("حداقل تعداد")]
    public int? MinQuantity { get; set; }

    [DisplayName("حداکثر تعداد")]
    public int? MaxQuantity { get; set; }

    [DisplayName("حداقل قیمت")]
    public int? MinPrice { get; set; }

    [DisplayName("حداکثر قیمت")]
    public int? MaxPrice { get; set; }

    [DisplayName("حداقل تخفیف")]
    public int? MinDiscountPercentage { get; set; }

    [DisplayName("حداکثر تخفیف")]
    public int? MaxDiscountPercentage { get; set; }

    [DisplayName("دارای موجودی")]
    public bool? OnlyAvailable { get; set; }

    [DisplayName("دارای تخفیف")]
    public bool? OnlyDiscounted { get; set; }
}