using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Api;
using Common.Api.Attributes;
using Common.Application.Utility;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Auth;
using Shop.API.ViewModels.Orders;
using Shop.Query.Users._DTOs;
using Shop.UI.Services.Auth;
using Shop.UI.Services.Orders;
using Shop.UI.Services.Users;
using Shop.UI.Setup.CookieUtility;
using Shop.UI.Setup.RazorUtility;
using Shop.UI.ViewModels.Auth;

namespace Shop.UI.Pages.Auth;

[BindProperties]
public class LoginModel : BaseRazorPage
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    private readonly IOrderService _orderService;
    private readonly CartCookieManager _cartCookieManager;

    public LoginModel(IUserService userService, IAuthService authService,
        IRazorToStringRenderer razorToStringRenderer, CartCookieManager cartCookieManager,
        IOrderService orderService) : base(razorToStringRenderer)
    {
        _userService = userService;
        _authService = authService;
        _cartCookieManager = cartCookieManager;
        _orderService = orderService;
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
            return await AjaxHtmlSuccessResultAsync("_Register", new RegisterUserViewModel());

        return await AjaxHtmlSuccessResultAsync("_Password", new PasswordViewModel());
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
        Response.Cookies.Append("token", token, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.Now.AddDays(5)
        });
        Response.Cookies.Append("refresh-token", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.Now.AddDays(30)
        });

        await SyncShopCart(token);

        var redirectTo = TempData["redirectTo"]?.ToString();
        if (!string.IsNullOrWhiteSpace(redirectTo) && Url.IsLocalUrl(redirectTo))
            return AjaxRedirectToPageResult(redirectTo);

        return AjaxRedirectToPageResult("../Index");
    }

    private async Task SyncShopCart(string token)
    {
        var shopCart = _cartCookieManager.GetCart();
        if (shopCart == null || shopCart.Items.Any() == false)
            return;

        HttpContext.Request.Headers.Append("Authorization", $"Bearer {token}");
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);

        var userId = Convert.ToInt64(jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        foreach (var item in shopCart.Items)
        {
            var result = await _orderService.AddItem(new AddOrderItemViewModel
            {
                UserId = userId,
                Quantity = item.Count,
                InventoryId = item.InventoryId
            });

            if (!result.IsSuccessful)
                MakeAlert(result);
        }
        _cartCookieManager.RemoveCart();
    }
}