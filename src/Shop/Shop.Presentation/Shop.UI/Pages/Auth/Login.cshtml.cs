using System.ComponentModel.DataAnnotations;
using Common.Api;
using Common.Api.Attributes;
using Common.Application.Utility;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Auth;
using Shop.Query.Users._DTOs;
using Shop.UI.Services.Auth;
using Shop.UI.Services.Users;
using Shop.UI.Setup.RazorUtility;
using Shop.UI.ViewModels.Auth;

namespace Shop.UI.Pages.Auth;

[BindProperties]
public class LoginModel : BaseRazorPage
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public LoginModel(IUserService userService, IAuthService authService,
        IRazorToStringRenderer razorToStringRenderer) : base(razorToStringRenderer)
    {
        _userService = userService;
        _authService = authService;
    }

    [Display(Name = "شماره موبایل یا ایمیل")]
    [Required(ErrorMessage = ValidationMessages.EmailOrPhoneRequired)]
    [RegularExpression(ValidationMessages.EmailOrPhoneRegex, ErrorMessage = ValidationMessages.InvalidEmailOrPhone)]
    [EmailOrIranPhone(ErrorMessage = ValidationMessages.InvalidEmailOrPhone)]
    public string EmailOrPhone { get; set; }

    public IActionResult OnGet(string redirectTo)
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToPage("../Index");

        TempData.Clear();
        TempData["redirectTo"] = redirectTo;
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _userService.SearchByEmailOrPhone(EmailOrPhone);

        if (result.IsSuccessful == false)
        {
            if (result.MetaData.ApiStatusCode == ApiStatusCode.TooManyRequests)
                Response.StatusCode = (int)result.MetaData.ApiStatusCode;
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }

        if (result.Data.NextStep is NextSteps.RegisterWithPhone)
        {
            result.MetaData.Message = "حساب کاربری با مشخصات وارد شده وجود ندارد. " +
                                      "لطفا از شماره تلفن همراه برای ساخت حساب کاربری استفاده نمایید.";
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }

        TempData["EmailOrPhone"] = EmailOrPhone;

        if (result.Data.NextStep is NextSteps.Register)
            return await AjaxSuccessHtmlResultAsync("_Register", new RegisterUserViewModel());

        return await AjaxSuccessHtmlResultAsync("_Password", new PasswordViewModel());
    }

    public async Task<IActionResult> OnPostPassword(PasswordViewModel viewModel)
    {
        var emailOrPhone = TempData["EmailOrPhone"]?.ToString();

        if (string.IsNullOrWhiteSpace(emailOrPhone))
            return AjaxRedirectToPageResult("Login");

        return await LoginUserAndAddTokenCookies(new LoginUserViewModel
        {
            EmailOrPhone = emailOrPhone,
            Password = viewModel.Password
        });
    }

    public async Task<IActionResult> OnPostRegister(RegisterUserViewModel model)
    {
        var emailOrPhone = TempData["EmailOrPhone"]?.ToString();

        if (string.IsNullOrWhiteSpace(emailOrPhone) || emailOrPhone.IsEmail())
            return AjaxRedirectToPageResult("Login");

        var registerResult = await _authService.Register(new RegisterUserViewModel
        {
            FullName = model.FullName,
            Gender = model.Gender,
            PhoneNumber = emailOrPhone,
            Password = model.Password,
            ConfirmPassword = model.ConfirmPassword
        });

        if (registerResult.IsSuccessful == false)
        {
            MakeAlert(registerResult);
            return AjaxErrorMessageResult(registerResult);
        }

        return await LoginUserAndAddTokenCookies(new LoginUserViewModel
        {
            EmailOrPhone = emailOrPhone,
            Password = model.Password
        });
    }

    private async Task<IActionResult> LoginUserAndAddTokenCookies(LoginUserViewModel model)
    {
        var loginResult = await _authService.Login(new LoginUserViewModel
        {
            EmailOrPhone = model.EmailOrPhone,
            Password = model.Password
        });

        if (loginResult.IsSuccessful == false)
        {
            MakeAlert(loginResult);
            return AjaxErrorMessageResult(loginResult);
        }

        var token = loginResult.Data.Token;
        var refreshToken = loginResult.Data.RefreshToken;
        Response.Cookies.Append("token", token);
        Response.Cookies.Append("refresh-token", refreshToken);

        var redirectTo = TempData["redirectTo"]?.ToString();
        if (!string.IsNullOrWhiteSpace(redirectTo) && Url.IsLocalUrl(redirectTo))
            return AjaxRedirectToPageResult(redirectTo);

        return AjaxRedirectToPageResult("../Index");
    }
}