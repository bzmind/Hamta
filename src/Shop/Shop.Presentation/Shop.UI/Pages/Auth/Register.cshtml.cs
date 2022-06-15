using System.ComponentModel.DataAnnotations;
using Common.Application.Utility;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.API.ViewModels.Auth;
using Shop.UI.Services.Auth;

namespace Shop.UI.Pages.Auth;

[BindProperties]
public class RegisterModel : PageModel
{
    private readonly IAuthService _authService;

    public RegisterModel(IAuthService authService)
    {
        _authService = authService;
    }

    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    [MinLength(8, ErrorMessage = "رمز عبور باید بیشتر از 7 کاراکتر باشد")]
    public string Password { get; set; }

    [Required(ErrorMessage = ValidationMessages.ConfirmPasswordRequired)]
    [Compare(nameof(Password), ErrorMessage = ValidationMessages.InvalidConfirmPassword)]
    public string ConfirmPassword { get; set; }

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

        if (emailOrPhone == null || emailOrPhone.ToString()!.IsEmail())
            return RedirectToPage("Login");

        var result = await _authService.Register(new RegisterViewModel
        {
            PhoneNumber = emailOrPhone.ToString()!,
            Password = Password,
            ConfirmPassword = ConfirmPassword
        });

        return Page();
    }
}