using System.ComponentModel.DataAnnotations;
using Common.Api;
using Common.Api.Attributes;
using Common.Application;
using Common.Application.Utility;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.API.ViewModels.Auth;
using Shop.Query.Users._DTOs;
using Shop.UI.Models;
using Shop.UI.Services.Auth;
using Shop.UI.Services.Users;
using Shop.UI.SetupClasses.ModelStateExtensions;

namespace Shop.UI.Pages.Auth;

[BindProperties]
[ValidateAntiForgeryToken]
public class LoginModel : PageModel
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public LoginModel(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [Display(Name = "شماره موبایل یا ایمیل")]
    [Required(ErrorMessage = ValidationMessages.EmailOrPhoneRequired)]
    [EmailOrIranPhone(ErrorMessage = ValidationMessages.InvalidEmailOrPhone)]
    public string EmailOrPhone { get; set; }

    public IActionResult OnGet()
    {
        TempData.Clear();
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var hasVisitedLogin = TempData["AlreadyVisitedLogin"];
        var hasVisitedRegister = TempData["AlreadyVisitedRegister"];

        if (hasVisitedLogin != null || hasVisitedRegister != null)
            return RedirectToPage("Login");

        var searchResult = await _userService.SearchByEmailOrPhone(EmailOrPhone);

        if (searchResult.StatusCode != OperationStatusCode.Success)
        {
            ModelState.AddModelError(string.Empty, searchResult.Message);
            return RedirectToPage("Login").WithModelStateOf(this);
        }

        TempData["AlreadyVisitedLogin"] = "true";
        TempData["EmailOrPhone"] = EmailOrPhone;

        if (searchResult.Data!.NextStep is LoginResult.NextSteps.Register)
            return Partial("_Register");

        return Partial("_Password");
    }

    public async Task<IActionResult> OnPostPassword(PasswordModel model)
    {
        var emailOrPhone = TempData["EmailOrPhone"]?.ToString();

        if (string.IsNullOrWhiteSpace(emailOrPhone))
            return RedirectToPage("Login");

        TempData.Clear();

        return await LoginUserAndAddTokenCookies(new LoginViewModel
        {
            EmailOrPhone = emailOrPhone,
            Password = model.Password
        }, "_Password");
    }

    public async Task<IActionResult> OnPostRegister(RegisterModel model)
    {
        var emailOrPhone = TempData["EmailOrPhone"]?.ToString();

        if (string.IsNullOrWhiteSpace(emailOrPhone) || emailOrPhone.IsEmail())
            return RedirectToPage("Login");

        TempData["AlreadyVisitedRegister"] = "true";

        var register = await _authService.Register(new RegisterViewModel
        {
            PhoneNumber = emailOrPhone,
            Password = model.Password,
            ConfirmPassword = model.ConfirmPassword
        });

        if (register.IsSuccessful == false)
        {
            ModelState.AddModelError(string.Empty, register.MetaData.Message);
            return Partial("_Register");
        }



        return await LoginUserAndAddTokenCookies(new LoginViewModel
        {
            EmailOrPhone = emailOrPhone,
            Password = model.Password
        }, "_Register");
    }

    private async Task<IActionResult> LoginUserAndAddTokenCookies(LoginViewModel model, string fallBackPage)
    {
        var loginResult = await _authService.Login(new LoginViewModel
        {
            EmailOrPhone = model.EmailOrPhone,
            Password = model.Password
        });

        if (!loginResult.IsSuccessful)
        {
            ModelState.AddModelError(string.Empty, loginResult.MetaData.Message);
            return Partial(fallBackPage);
        }

        var token = loginResult.Data.Token;
        var refreshToken = loginResult.Data.RefreshToken;
        HttpContext.Response.Cookies.Append("token", token);
        HttpContext.Response.Cookies.Append("refresh-token", refreshToken);

        return RedirectToPage("../Index");
    }
}