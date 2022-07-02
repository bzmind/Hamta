using System.ComponentModel.DataAnnotations;
using Common.Api;
using Common.Api.Attributes;
using Common.Application.Utility;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Auth;
using Shop.Query.Users._DTOs;
using Shop.UI.Models.Auth;
using Shop.UI.Services.Auth;
using Shop.UI.Services.Users;
using Shop.UI.SetupClasses.RazorUtility;

namespace Shop.UI.Pages.Auth;

// TODO: Api project is still using the old model for login, it asks for emailOrPhone and also password which is wrong, fix it, make it like the UI project
[BindProperties]
public class LoginModel : BaseRazorPage
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    private readonly IRazorToStringRenderer _razorToStringRenderer;

    public LoginModel(IUserService userService, IAuthService authService,
        IRazorToStringRenderer razorToStringRenderer)
    {
        _userService = userService;
        _authService = authService;
        _razorToStringRenderer = razorToStringRenderer;
    }

    [Display(Name = "شماره موبایل یا ایمیل")]
    [Required(ErrorMessage = ValidationMessages.EmailOrPhoneRequired)]
    [RegularExpression(ValidationMessages.EmailOrPhoneRegex, ErrorMessage = ValidationMessages.InvalidEmailOrPhone)]
    [EmailOrIranPhone(ErrorMessage = ValidationMessages.InvalidEmailOrPhone)]
    public string EmailOrPhone { get; set; }

    public IActionResult OnGet()
    {
        TempData.Clear();
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
            return AjaxMessageResult(result);
        }

        if (result.Data.NextStep is LoginNextStep.NextSteps.RegisterWithPhone)
        {
            result.MetaData.Message = "حساب کاربری با مشخصات وارد شده وجود ندارد. " +
                                         "لطفا از شماره تلفن همراه برای ساخت حساب کاربری استفاده نمایید.";
            MakeAlert(result);
            return AjaxMessageResult(result);
        }

        TempData["EmailOrPhone"] = EmailOrPhone;

        if (result.Data.NextStep is LoginNextStep.NextSteps.Register)
            return AjaxHtmlResult(ApiResult<string>.Success(await RegisterPageHtml()));

        return AjaxHtmlResult(ApiResult<string>.Success(await PasswordPageHtml()));
    }

    public async Task<IActionResult> OnPostPassword(PasswordModel model)
    {
        var emailOrPhone = TempData.Peek("EmailOrPhone")?.ToString();

        if (string.IsNullOrWhiteSpace(emailOrPhone))
            return AjaxRedirectToPageResult("Login");

        return await LoginUserAndAddTokenCookies(new LoginViewModel
        {
            EmailOrPhone = emailOrPhone,
            Password = model.Password
        });
    }

    public async Task<IActionResult> OnPostRegister(RegisterModel model)
    {
        var emailOrPhone = TempData.Peek("EmailOrPhone")?.ToString();

        if (string.IsNullOrWhiteSpace(emailOrPhone) || emailOrPhone.IsEmail())
            return AjaxRedirectToPageResult("Login");

        var registerResult = await _authService.Register(new RegisterViewModel
        {
            PhoneNumber = emailOrPhone,
            Password = model.Password,
            ConfirmPassword = model.ConfirmPassword
        });

        if (registerResult.IsSuccessful == false)
        {
            MakeAlert(registerResult);
            return AjaxMessageResult(registerResult);
        }

        return await LoginUserAndAddTokenCookies(new LoginViewModel
        {
            EmailOrPhone = emailOrPhone,
            Password = model.Password
        });
    }

    private async Task<IActionResult> LoginUserAndAddTokenCookies(LoginViewModel model)
    {
        var loginResult = await _authService.Login(new LoginViewModel
        {
            EmailOrPhone = model.EmailOrPhone,
            Password = model.Password
        });

        if (loginResult.IsSuccessful == false)
        {
            MakeAlert(loginResult);
            return AjaxMessageResult(loginResult);
        }

        TempData.Clear();

        var token = loginResult.Data.Token;
        var refreshToken = loginResult.Data.RefreshToken;
        Response.Cookies.Append("token", token);
        Response.Cookies.Append("refresh-token", refreshToken);

        return AjaxRedirectToPageResult("../Index");
    }

    private async Task<string> PasswordPageHtml()
    {
        return await _razorToStringRenderer.RenderToStringAsync("_Password", new PasswordModel(), PageContext);
    }

    private async Task<string> RegisterPageHtml()
    {
        return await _razorToStringRenderer.RenderToStringAsync("_Register", new RegisterModel(), PageContext);
    }
}