using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Sellers;

public class EditSellerViewModel
{
    [DisplayName("نام فروشگاه")]
    [Required(ErrorMessage = ValidationMessages.ShopNameRequired)]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string ShopName { get; set; }

    [DisplayName("کدملی")]
    [Required(ErrorMessage = ValidationMessages.NationalCodeRequired)]
    [MaxLength(10, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    [RegularExpression(ValidationMessages.OnlyNumberRegex, ErrorMessage = ValidationMessages.InvalidNationalCode)]
    public string NationalCode { get; set; }
}