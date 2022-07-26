using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Sellers.Inventories;

public class AddSellerInventoryViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long ProductId { get; set; }

    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long ColorId { get; set; }

    [DisplayName("تعداد محصولات")]
    [Required(ErrorMessage = ValidationMessages.QuantityRequired)]
    public int Quantity { get; set; }

    [DisplayName("قیمت محصول")]
    [Required(ErrorMessage = ValidationMessages.PriceRequired)]
    public int Price { get; set; }

    [DisplayName("درصد تخفیف")]
    public int? DiscountPercentage { get; set; } = 0;
}