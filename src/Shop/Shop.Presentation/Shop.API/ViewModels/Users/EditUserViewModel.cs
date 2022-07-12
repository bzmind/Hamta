using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;
using Shop.Domain.UserAggregate;

namespace Shop.API.ViewModels.Users;

public class EditUserViewModel
{
    [Display(Name = "نام و نام خانوادگی")]
    [Required(ErrorMessage = ValidationMessages.FullNameRequired)]
    [MaxLength(30, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string FullName { get; set; }

    [DisplayName("ایمیل")]
    [Required(ErrorMessage = ValidationMessages.EmailRequired)]
    [RegularExpression(ValidationMessages.EmailRegex, ErrorMessage = ValidationMessages.InvalidEmail)]
    public string Email { get; set; }

    [Display(Name = "شماره موبایل")]
    [Required(ErrorMessage = ValidationMessages.PhoneNumberRequired)]
    [IranPhone(ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    [MaxLength(11, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string PhoneNumber { get; set; }

    [Display(Name = "جنسیت")]
    [Required(ErrorMessage = ValidationMessages.GenderRequired)]
    [EnumNotNullOrZero(ErrorMessage = ValidationMessages.InvalidGender)]
    public User.UserGender Gender { get; set; }
}