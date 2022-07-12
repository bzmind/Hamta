using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Inventories;

public class SetInventoryDiscountPercentageViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long InventoryId { get; set; }

    [DisplayName("تخفیف")]
    [Required(ErrorMessage = ValidationMessages.QuantityRequired)]
    [MinLength(1, ErrorMessage = "{0} باید بیشتر از 0 درصد باشد")]
    [MaxLength(100, ErrorMessage = "{0} باید کمتر یا مساوی 100 درصد باشد")]
    public int DiscountPercentage  { get; set; }
}