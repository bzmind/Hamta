using Common.Api;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Entities.Slider;
using Shop.Query.Entities._DTOs;
using Shop.UI.Services.Entities.Sliders;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Sliders;

[BindProperties]
public class IndexModel : BaseRazorPage
{
    private readonly ISliderService _sliderService;

    public IndexModel(IRazorToStringRenderer razorToStringRenderer,
        ISliderService sliderService) : base(razorToStringRenderer)
    {
        _sliderService = sliderService;
    }

    public List<SliderDto> Sliders { get; set; }

    public async Task OnGet()
    {
        Sliders = await GetData(async () => await _sliderService.GetAll());
    }

    public async Task<IActionResult> OnPost(CreateSliderViewModel viewModel)
    {
        var result = await _sliderService.Create(viewModel);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxRedirectToPageResult();
    }

    public async Task<IActionResult> OnGetShowAddPage()
    {
        return await AjaxHtmlSuccessResultAsync("_Add", new CreateSliderViewModel());
    }

    public async Task<IActionResult> OnPostEditSlider(EditSliderViewModel model)
    {
        var result = await _sliderService.Edit(model);
        if (result.IsSuccessful == false)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxRedirectToPageResult();
    }

    public async Task<IActionResult> OnGetShowEditPage(long id)
    {
        var slider = await GetData(async () => await _sliderService.GetById(id));
        if (slider == null)
            return AjaxErrorMessageResult(ValidationMessages.FieldNotFound("اسلایدر"), ApiStatusCode.NotFound);

        var model = new EditSliderViewModel
        {
            Id = slider.Id,
            Title = slider.Title,
            Link = slider.Link,
            PreviousImageName = slider.Image
        };
        return await AjaxHtmlSuccessResultAsync("_Edit", model);
    }

    public async Task<IActionResult> OnPostRemoveSlider(long id)
    {
        var result = await _sliderService.Remove(id);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxRedirectToPageResult();
    }
}