using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;
using Shop.Domain.UserAggregate;

namespace Shop.API.ViewModels.Users.Auth;

public class RegisterUserViewModel
{
    [Display(Name = "نام و نام خانوادگی")]
    [Required(ErrorMessage = ValidationMessages.FullNameRequired)]
    [MaxLength(30, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string FullName { get; set; }

    [Display(Name = "جنسیت")]
    [Required(ErrorMessage = ValidationMessages.GenderRequired)]
    [EnumNotNullOrZero(ErrorMessage = ValidationMessages.InvalidGender)]
    public User.UserGender Gender { get; set; }

    [Display(Name = "شماره موبایل")]
    [Required(ErrorMessage = ValidationMessages.PhoneNumberRequired)]
    [IranPhone(ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    [MaxLength(11, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string PhoneNumber { get; set; }

    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    [MinLength(8, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد")]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}