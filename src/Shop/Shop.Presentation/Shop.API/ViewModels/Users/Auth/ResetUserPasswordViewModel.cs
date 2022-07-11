using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Users.Auth;

public class ResetUserPasswordViewModel
{
    [DisplayName("رمز عبور فعلی")]
    [Required(ErrorMessage = ValidationMessages.CurrentPasswordRequired)]
    [MinLength(8, ErrorMessage = "{0} باید بیشتر از 7 کاراکتر باشد")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [DisplayName("رمز عبور جدید")]
    [Required(ErrorMessage = ValidationMessages.NewPasswordRequired)]
    [MinLength(8, ErrorMessage = "{0} باید بیشتر از 7 کاراکتر باشد")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [DisplayName("تکرار رمز عبور جدید")]
    [Required(ErrorMessage = ValidationMessages.ConfirmNewPasswordRequired)]
    [MinLength(8, ErrorMessage = "{0} باید بیشتر از 7 کاراکتر باشد")]
    [Compare(nameof(NewPassword), ErrorMessage = ValidationMessages.InvalidConfirmNewPassword)]
    [DataType(DataType.Password)]
    public string ConfirmNewPassword { get; set; }
}