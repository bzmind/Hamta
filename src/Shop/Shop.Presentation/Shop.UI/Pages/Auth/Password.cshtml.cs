﻿using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.UI.Models.Auth;
using Shop.UI.Services.Auth;

namespace Shop.UI.Pages.Auth;

[BindProperties]
public class PasswordModel : PageModel
{
    private readonly IAuthService _authService;

    public PasswordModel(IAuthService authService)
    {
        _authService = authService;
    }

    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    [MinLength(8, ErrorMessage = "رمز عبور باید بیشتر از 7 کاراکتر باشد")]
    public string Password { get; set; }

    public IActionResult OnGet()
    {
        var emailOrPhone = TempData.Peek("EmailOrPhone");

        if (emailOrPhone == null)
            return RedirectToPage("Login");

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var emailOrPhone = TempData["EmailOrPhone"];

        if (emailOrPhone == null)
            return RedirectToPage("Login");

        var result = await _authService.Login(new LoginViewModel
        {
            EmailOrPhone = emailOrPhone.ToString()!,
            Password = Password
        });

        return Page();
    }
}