using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Users.Addresses;

public class EditUserAddressViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseUserAddress)]
    public long Id { get; set; }

    [Display(Name = "نام و نام خانوادگی")]
    [Required(ErrorMessage = ValidationMessages.FullNameRequired)]
    [MaxLength(30, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string FullName { get; set; }

    [Display(Name = "شماره موبایل")]
    [Required(ErrorMessage = ValidationMessages.PhoneNumberRequired)]
    [IranPhone(ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    [MaxLength(11, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string PhoneNumber { get; set; }

    [Display(Name = "استان")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(30, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Province { get; set; }

    [Display(Name = "شهر")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(30, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string City { get; set; }

    [Display(Name = "آدرس کامل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(300, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string FullAddress { get; set; }

    [Display(Name = "کد پستی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(10, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string PostalCode { get; set; }
}