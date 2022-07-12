using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Users.Auth;

public class ResetUserPasswordViewModel
{
    [DisplayName("رمز عبور فعلی")]
    [Required(ErrorMessage = ValidationMessages.CurrentPasswordRequired)]
    [MinLength(8, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد")]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [DisplayName("رمز عبور جدید")]
    [Required(ErrorMessage = ValidationMessages.NewPasswordRequired)]
    [MinLength(8, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد")]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Display(Name = "تکرار رمز عبور جدید")]
    [Required(ErrorMessage = ValidationMessages.ConfirmPasswordRequired)]
    [Compare(nameof(NewPassword), ErrorMessage = ValidationMessages.InvalidConfirmPassword)]
    [MinLength(8, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد")]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    [DataType(DataType.Password)]
    public string ConfirmNewPassword { get; set; }
}