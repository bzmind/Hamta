using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Query.Users._DTOs;
using Shop.UI.Services.Users;

namespace Shop.UI.Pages.Auth;

[BindProperties]
public class LoginModel : PageModel
{
    private readonly IUserService _userService;

    public LoginModel(IUserService userService)
    {
        _userService = userService;
    }

    [Required(ErrorMessage = ValidationMessages.EmailOrPhoneRequired)]
    [EmailOrIranPhone(ErrorMessage = ValidationMessages.InvalidEmailOrPhone)]
    public string EmailOrPhone { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _userService.SearchByEmailOrPhone(EmailOrPhone);

        if (result.StatusCode != OperationStatusCode.Success)
        {
            ModelState.AddModelError(nameof(EmailOrPhone), result.Message);
            return Page();
        }

        TempData["EmailOrPhone"] = EmailOrPhone;

        if (result.Data!.NextStep is LoginResult.NextSteps.Register)
            return RedirectToPage("Register");

        return RedirectToPage("Password");
    }
}