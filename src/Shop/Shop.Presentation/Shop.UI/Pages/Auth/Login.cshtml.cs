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
using Shop.UI.SetupClasses.ModelStateExtensions;
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

    public async Task<IActionResult> OnGetShowPasswordPage()
    {
        var page = await _razorToStringRenderer.RenderToStringAsync("_Password", new PasswordModel(), PageContext);
        return Content(page);
    }

    public async Task<IActionResult> OnGetShowRegisterPage()
    {
        var page = await _razorToStringRenderer.RenderToStringAsync("_Register", new RegisterModel(), PageContext);
        return Content(page);
    }
    
    public async Task<IActionResult> OnPost()
    {
        var apiResult = await _userService.SearchByEmailOrPhone(EmailOrPhone);

        if (apiResult.Data.NextStep is LoginNextStep.NextSteps.RegisterWithPhone)
        {
            apiResult.MetaData.Message = "حساب کاربری با مشخصات وارد شده وجود ندارد. " +
                                         "لطفا از شماره تلفن همراه برای ساخت حساب کاربری استفاده نمایید.";
            MakeAlert(apiResult);
            return AjaxResultJson(apiResult, false);
        }

        TempData["EmailOrPhone"] = EmailOrPhone;

        if (apiResult.Data.NextStep is LoginNextStep.NextSteps.Register)
        {
            var registerPage = await _razorToStringRenderer.RenderToStringAsync
                ("_Register", new RegisterModel(), PageContext);
            return AjaxResultJson(ApiResult<string>.Success(registerPage), true);
        }

        var passwordPage = await _razorToStringRenderer.RenderToStringAsync
            ("_Password", new PasswordModel(), PageContext);
        return AjaxResultJson(ApiResult<string>.Success(passwordPage), true);
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
        }, "Password");
    }

    public async Task<IActionResult> OnPostRegister(RegisterModel model)
    {
        var emailOrPhone = TempData["EmailOrPhone"]?.ToString();

        if (string.IsNullOrWhiteSpace(emailOrPhone) || emailOrPhone.IsEmail())
            return RedirectToPage("Login");

        var register = await _authService.Register(new RegisterViewModel
        {
            PhoneNumber = emailOrPhone,
            Password = model.Password,
            ConfirmPassword = model.ConfirmPassword
        });

        if (register.IsSuccessful == false)
        {
            ModelState.AddModelError(string.Empty, register.MetaData.Message);
            return RedirectToPage("Login", "Register").WithModelStateOf(this);
        }

        return await LoginUserAndAddTokenCookies(new LoginViewModel
        {
            EmailOrPhone = emailOrPhone,
            Password = model.Password
        }, "Register");
    }

    private async Task<IActionResult> LoginUserAndAddTokenCookies(LoginViewModel model, string fallBackHandler)
    {
        var loginResult = await _authService.Login(new LoginViewModel
        {
            EmailOrPhone = model.EmailOrPhone,
            Password = model.Password
        });

        if (!loginResult.IsSuccessful)
        {
            ModelState.AddModelError(string.Empty, loginResult.MetaData.Message);
            return RedirectToPage("Login", fallBackHandler).WithModelStateOf(this);
        }

        var token = loginResult.Data.Token;
        var refreshToken = loginResult.Data.RefreshToken;
        HttpContext.Response.Cookies.Append("token", token);
        HttpContext.Response.Cookies.Append("refresh-token", refreshToken);

        return RedirectToPage("../Index");
    }
}