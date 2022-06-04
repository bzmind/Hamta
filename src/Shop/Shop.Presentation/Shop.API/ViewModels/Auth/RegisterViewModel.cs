using System.ComponentModel.DataAnnotations;
using Common.Api.Validation;
using Common.Application.Validation;

namespace Shop.API.ViewModels.Auth;

public class RegisterViewModel
{
    [MaxLength(20, ErrorMessage = "نام و نام خانوادگی باید کمتر از 20 کاراکتر باشد")]
    public string FullName { get; set; }

    [Required(ErrorMessage = ValidationMessages.PhoneNumberRequired)]
    [IranPhoneNumber]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    [MinLength(8, ErrorMessage = "رمز عبور باید بیشتر از 7 کاراکتر باشد")]
    public string Password { get; set; }

    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    [MinLength(8, ErrorMessage = "رمز عبور باید بیشتر از 7 کاراکتر باشد")]
    [Compare(nameof(Password), ErrorMessage = "رمز های عبور یکسان نیستند")]
    public string ConfirmPassword { get; set; }

    [EmailAddress(ErrorMessage = ValidationMessages.InvalidEmail)]
    public string Email { get; set; }
}