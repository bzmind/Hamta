using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Auth;

public class LoginUserViewModel
{
    [EmailOrIranPhone(ErrorMessage = ValidationMessages.InvalidEmailOrPhone)]
    [Required(ErrorMessage = ValidationMessages.EmailOrPhoneRequired)]
    public string EmailOrPhone { get; set; }

    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    [MinLength(8, ErrorMessage = "رمز عبور باید بیشتر از 7 کاراکتر باشد")]
    public string Password { get; set; }
}