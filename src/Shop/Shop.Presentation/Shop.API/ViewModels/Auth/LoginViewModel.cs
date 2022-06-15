﻿using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Auth;

public class LoginViewModel
{
    [EmailOrIranPhone(ErrorMessage = ValidationMessages.InvalidEmailOrPhone)]
    [Required(ErrorMessage = ValidationMessages.EmailOrPhoneRequired)]
    public string EmailOrPhone { get; set; }

    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    public string Password { get; set; }
}