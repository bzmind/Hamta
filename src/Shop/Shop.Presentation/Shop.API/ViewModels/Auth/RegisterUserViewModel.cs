using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;
using Shop.Domain.UserAggregate;

namespace Shop.API.ViewModels.Auth;

public class RegisterUserViewModel
{
    [Display(Name = "نام و نام خانوادگی")]
    [Required(ErrorMessage = ValidationMessages.FullNameRequired)]
    public string FullName { get; set; }

    [Display(Name = "جنسیت")]
    [Required(ErrorMessage = ValidationMessages.GenderRequired)]
    [EnumNotNullOrZero(ErrorMessage = ValidationMessages.InvalidGender)]
    public User.UserGender Gender { get; set; }

    [Display(Name = "شماره موبایل")]
    [Required(ErrorMessage = ValidationMessages.PhoneNumberRequired)]
    [IranPhone(ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    public string PhoneNumber { get; set; }

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