using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Colors;

public class CreateColorViewModel
{
    [DisplayName("نام رنگ")]
    [Required(ErrorMessage = ValidationMessages.ColorNameRequired)]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Name { get; set; }

    [DisplayName("کد رنگ")]
    [Required(ErrorMessage = ValidationMessages.ColorCodeRequired)]
    [MinLength(7, ErrorMessage = "{0} باید حداقل 7 کاراکتر باشد")]
    [MaxLength(7, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Code { get; set; }
}