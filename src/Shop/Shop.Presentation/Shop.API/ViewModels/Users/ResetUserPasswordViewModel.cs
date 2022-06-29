using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Users;

public class ResetUserPasswordViewModel
{
    [Required(ErrorMessage = ValidationMessages.CurrentPasswordRequired)]
    [MinLength(8, ErrorMessage = "رمز عبور فعلی باید بیشتر از 7 کاراکتر باشد")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = ValidationMessages.NewPasswordRequired)]
    [MinLength(8, ErrorMessage = "رمز عبور جدید باید بیشتر از 7 کاراکتر باشد")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = ValidationMessages.ConfirmPasswordRequired)]
    [MinLength(8, ErrorMessage = "تکرار رمز عبور جدید باید بیشتر از 7 کاراکتر باشد")]
    [Compare(nameof(NewPassword), ErrorMessage = ValidationMessages.InvalidConfirmPassword)]
    public string ConfirmPassword { get; set; }
}