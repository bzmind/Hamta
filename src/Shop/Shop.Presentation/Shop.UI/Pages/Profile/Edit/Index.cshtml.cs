using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Api;
using Common.Api.Attributes;
using Common.Api.Utility;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.CommandViewModels.Users;
using Shop.API.ViewModels.Users;
using Shop.UI.Services.Users;
using Shop.UI.SetupClasses.ModelStateExtensions;
using Shop.UI.SetupClasses.RazorUtility;

namespace Shop.UI.Pages.Profile.Edit;

[Authorize]
[BindProperties]
public class IndexModel : BaseRazorPage
{
    private readonly IUserService _userService;
    private readonly IRazorToStringRenderer _razorToStringRenderer;

    public IndexModel(IUserService userService, IRazorToStringRenderer razorToStringRenderer)
    {
        _userService = userService;
        _razorToStringRenderer = razorToStringRenderer;
    }

    [DisplayName("نام و نام خانوادگی")]
    [Required(ErrorMessage = ValidationMessages.FullNameRequired)]
    public string FullName { get; set; }

    [DisplayName("ایمیل")]
    [Required(ErrorMessage = ValidationMessages.EmailRequired)]
    [RegularExpression(ValidationMessages.EmailRegex, ErrorMessage = ValidationMessages.InvalidEmail)]
    public string Email { get; set; }

    [DisplayName("شماره موبایل")]
    [Required(ErrorMessage = ValidationMessages.PhoneNumberRequired)]
    [RegularExpression(ValidationMessages.IranPhoneRegex, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    [IranPhone(ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    public string PhoneNumber { get; set; }

    public async Task OnGet()
    {
        var user = await _userService.GetById(User.GetUserId());
        FullName = user.FullName;
        Email = user.Email;
        PhoneNumber = user.PhoneNumber.Value;
        ViewData["IsSubscribedToNewsletter"] = user.IsSubscribedToNewsletter;
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _userService.Edit(new EditUserCommandViewModel
        {
            FullName = FullName,
            Email = Email,
            PhoneNumber = PhoneNumber
        });

        return RedirectToPage("../Index");
    }

    public async Task<IActionResult> OnPostResetPassword(ResetUserPasswordViewModel model)
    {
        var result = await _userService.ResetPassword(model);

        if (result.IsSuccessful == false)
        {
            ModelState.Clear();
            ModelState.AddModelError(nameof(model.CurrentPassword), result.MetaData.Message);
            return RedirectToPage().WithModelStateOf(this);
        }

        return RedirectToPage("../Auth/Login");
    }

    public async Task<IActionResult> OnPostSetNewsletterSubscription()
    {
        var result = await _userService.SetNewsletterSubscription(User.GetUserId());

        if (result.IsSuccessful == false)
        {
            MakeAlert(result);
            if (result.MetaData.ApiStatusCode == ApiStatusCode.TooManyRequests)
                Response.StatusCode = (int)result.MetaData.ApiStatusCode;
            return AjaxMessageResult(result);
        }

        if (result.Data == false)
        {
            return AjaxHtmlResult(ApiResult<string>.Success
                (await _razorToStringRenderer.RenderToStringAsync("_NotSubscribedToNewsletter", null, PageContext)));
        }

        return AjaxHtmlResult(ApiResult<string>.Success
            (await _razorToStringRenderer.RenderToStringAsync("_SubscribedToNewsletter", null, PageContext)));
    }
}