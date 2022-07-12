using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Api;
using Common.Api.Attributes;
using Common.Api.Utility;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shop.API.ViewModels.Users;
using Shop.API.ViewModels.Users.Auth;
using Shop.Domain.UserAggregate;
using Shop.UI.Services.Avatars;
using Shop.UI.Services.Users;
using Shop.UI.Setup.ModelStateExtensions;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Profile.Edit;

[BindProperties]
public class IndexModel : BaseRazorPage
{
    private readonly IUserService _userService;
    private readonly IAvatarService _avatarService;

    public IndexModel(IUserService userService, IAvatarService avatarService,
        IRazorToStringRenderer razorToStringRenderer) : base(razorToStringRenderer)
    {
        _userService = userService;
        _avatarService = avatarService;
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

    [Display(Name = "جنسیت")]
    [Required(ErrorMessage = ValidationMessages.GenderRequired)]
    [EnumNotNullOrZero(ErrorMessage = ValidationMessages.InvalidGender)]
    public User.UserGender Gender { get; set; }

    [BindNever]
    public string AvatarName { get; set; }
    [BindNever]
    public bool IsSubscribedToNewsletter { get; set; }

    public async Task OnGet()
    {
        var user = await _userService.GetById(User.GetUserId());
        FullName = user.FullName;
        Email = user.Email;
        PhoneNumber = user.PhoneNumber.Value;
        Gender = user.Gender;
        AvatarName = user.Avatar.Name;
        IsSubscribedToNewsletter = user.IsSubscribedToNewsletter;
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _userService.Edit(new EditUserViewModel
        {
            FullName = FullName,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Gender = Gender
        });

        if (result.IsSuccessful == false)
        {
            MakeAlert(result);
            return RedirectToPage().WithModelStateOf(this);
        }

        return RedirectToPage("Index");
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

        return RedirectToPage("../Index");
    }

    public async Task<IActionResult> OnPostSetNewsletterSubscription()
    {
        var result = await _userService.SetNewsletterSubscription(User.GetUserId());

        if (result.IsSuccessful == false)
        {
            MakeAlert(result);
            if (result.MetaData.ApiStatusCode == ApiStatusCode.TooManyRequests)
                Response.StatusCode = (int)result.MetaData.ApiStatusCode;
            return AjaxErrorMessageResult(result);
        }

        if (result.Data == false)
            return await SuccessResultWithPageHtml("_NotSubscribedToNewsletter", null);

        return await SuccessResultWithPageHtml("_SubscribedToNewsletter", null);
    }

    public async Task<IActionResult> OnGetShowAvatarsModal()
    {
        var avatars = await _avatarService.GetAll();
        return await SuccessResultWithPageHtml("_AvatarsModal", avatars);
    }

    public async Task<IActionResult> OnPostSetAvatar(long avatarId)
    {
        var result = await _userService.SetAvatar(avatarId);
        if (result.IsSuccessful == false)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }

        return AjaxRedirectToPageResult("Index");
    }
}