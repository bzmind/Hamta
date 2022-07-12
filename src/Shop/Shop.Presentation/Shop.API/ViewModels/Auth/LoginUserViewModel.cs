using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Auth;

public class LoginUserViewModel
{
    [DisplayName("شماره موبایل یا ایمیل")]
    [EmailOrIranPhone(ErrorMessage = ValidationMessages.InvalidEmailOrPhone)]
    [Required(ErrorMessage = ValidationMessages.EmailOrPhoneRequired)]
    [MaxLength(250, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string EmailOrPhone { get; set; }

    [DisplayName("رمز عبور")]
    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    [MinLength(8, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد")]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Password { get; set; }
}