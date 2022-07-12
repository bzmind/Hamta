using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Inventories;

public class CreateInventoryViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long ProductId { get; set; }

    [DisplayName("تعداد محصولات")]
    [Required(ErrorMessage = ValidationMessages.QuantityRequired)]
    [MinLength(1, ErrorMessage = "{0} باید بیشتر از 0 باشد")]
    public int Quantity { get; set; }

    [DisplayName("قیمت محصول")]
    [Required(ErrorMessage = ValidationMessages.PriceRequired)]
    [MinLength(1, ErrorMessage = "{0} باید بیشتر از 0 تومان باشد")]
    public int Price { get; set; }

    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long ColorId { get; set; }
}