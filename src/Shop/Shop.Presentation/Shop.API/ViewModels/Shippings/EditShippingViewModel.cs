using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Shippings;

public class EditShippingViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseShipping)]
    public long Id { get; set; }

    [Display(Name = "نام روش ارسال")]
    [Required(ErrorMessage = ValidationMessages.NameRequired)]
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Name { get; set; }

    [Display(Name = "هزینه روش ارسال")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MinLength(0, ErrorMessage = "{0} باید بیشتر از 0 باشد")]
    public int Cost { get; set; }
}