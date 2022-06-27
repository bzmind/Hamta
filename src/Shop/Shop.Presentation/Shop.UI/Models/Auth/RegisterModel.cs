using Common.Application.Utility.Validation;
using System.ComponentModel.DataAnnotations;

namespace Shop.UI.Models.Auth;

public class RegisterModel
{
    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    [MinLength(8, ErrorMessage = "{0} باید بیشتر از 7 کاراکتر باشد")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "تکرار رمز عبور")]
    [Required(ErrorMessage = ValidationMessages.ConfirmPasswordRequired)]
    [Compare(nameof(Password), ErrorMessage = ValidationMessages.InvalidConfirmPassword)]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}