using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;
using static System.Int32;

namespace Shop.API.ViewModels.Sellers.Inventories;

public class AddSellerInventoryViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseProduct)]
    public long ProductId { get; set; }

    [Required(ErrorMessage = ValidationMessages.ChooseColor)]
    public long ColorId { get; set; }

    [DisplayName("تعداد")]
    [Required(ErrorMessage = ValidationMessages.QuantityRequired)]
    public int Quantity { get; set; }

    [DisplayName("قیمت اصلی")]
    [Required(ErrorMessage = ValidationMessages.PriceRequired)]
    [Range(0, MaxValue, ErrorMessage = ValidationMessages.InvalidPrice)]
    public int Price { get; set; }

    [DisplayName("درصد تخفیف")]
    [Required(ErrorMessage = ValidationMessages.DiscountPercentageRequired)]
    [Range(0, 100, ErrorMessage = ValidationMessages.InvalidDiscountPercentageRange)]
    public int DiscountPercentage { get; set; }
}