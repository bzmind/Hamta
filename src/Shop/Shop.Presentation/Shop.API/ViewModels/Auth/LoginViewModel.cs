using System.ComponentModel.DataAnnotations;
using Common.Api.Validation;
using Common.Application.Validation;

namespace Shop.API.ViewModels.Auth;

public class LoginViewModel
{
    [EmailOrPhone]
    public string EmailOrPhoneNumber { get; set; }

    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    public string Password { get; set; }
}