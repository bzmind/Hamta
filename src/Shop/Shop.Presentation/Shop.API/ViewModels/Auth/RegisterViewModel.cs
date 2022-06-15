﻿using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Auth;

public class RegisterViewModel
{
    [IranPhone]
    [Required(ErrorMessage = ValidationMessages.PhoneNumberRequired)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    [MinLength(8, ErrorMessage = "رمز عبور باید بیشتر از 7 کاراکتر باشد")]
    public string Password { get; set; }

    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    [MinLength(8, ErrorMessage = "رمز عبور باید بیشتر از 7 کاراکتر باشد")]
    [Compare(nameof(Password), ErrorMessage = "رمز های عبور یکسان نیستند")]
    public string ConfirmPassword { get; set; }
}