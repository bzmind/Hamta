using Common.Api;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Entities.Banner;
using Shop.Query.Entities._DTOs;
using Shop.UI.Services.Entities.Banners;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Banners;

[BindProperties]
public class IndexModel : BaseRazorPage
{
    private readonly IBannerService _bannerService;

    public IndexModel(IRazorToStringRenderer razorToStringRenderer,
        IBannerService bannerService) : base(razorToStringRenderer)
    {
        _bannerService = bannerService;
    }

    public List<BannerDto> Banners { get; set; }

    public async Task<IActionResult> OnGet()
    {
        Banners = await _bannerService.GetAll();
        return Page();
    }

    public async Task<IActionResult> OnPost(CreateBannerViewModel viewModel)
    {
        var result = await _bannerService.Create(viewModel);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxRedirectToPageResult();
    }

    public async Task<IActionResult> OnGetShowAddPage()
    {
        return await AjaxHtmlSuccessResultAsync("_Add", new CreateBannerViewModel());
    }

    public async Task<IActionResult> OnPostEditBanner(EditBannerViewModel model)
    {
        var result = await _bannerService.Edit(model);
        if (result.IsSuccessful == false)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxRedirectToPageResult();
    }

    public async Task<IActionResult> OnGetShowEditPage(long id)
    {
        var banner = await _bannerService.GetById(id);
        if (banner == null)
        {
            MakeAlert(ValidationMessages.FieldNotFound("بنر"));
            return AjaxErrorMessageResult(ValidationMessages.FieldNotFound("بنر"), ApiStatusCode.NotFound);
        }

        var model = new EditBannerViewModel
        {
            Id = banner.Id,
            Link = banner.Link,
            PreviousImageName = banner.Image,
            Position = banner.Position
        };
        return await AjaxHtmlSuccessResultAsync("_Edit", model);
    }

    public async Task<IActionResult> OnPostRemoveBanner(long id)
    {
        var result = await _bannerService.Remove(id);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxRedirectToPageResult();
    }
}