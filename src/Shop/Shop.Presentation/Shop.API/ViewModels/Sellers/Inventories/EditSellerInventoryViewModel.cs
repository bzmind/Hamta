using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Sellers.Inventories;

public class EditSellerInventoryViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseSellerInventory)]
    public long InventoryId { get; set; }

    [Required(ErrorMessage = ValidationMessages.ChooseProduct)]
    public long ProductId { get; set; }

    [Required(ErrorMessage = ValidationMessages.ChooseColor)]
    public long ColorId { get; set; }

    [DisplayName("تعداد")]
    [Required(ErrorMessage = ValidationMessages.QuantityRequired)]
    public int Quantity { get; set; }

    [DisplayName("قیمت")]
    [Required(ErrorMessage = ValidationMessages.PriceRequired)]
    public int Price { get; set; }

    [DisplayName("درصد تخفیف")]
    public int DiscountPercentage { get; set; } = 0;

    public int OriginalPrice => Price + Price * DiscountPercentage / 100;
}