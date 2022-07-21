using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.UI.ViewModels.Auth;

public class PasswordViewModel
{
    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    [MinLength(8, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}