using Common.Api;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Shippings;
using Shop.Query.Shippings._DTOs;
using Shop.UI.Services.Shippings;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Shippings;

public class IndexModel : BaseRazorPage
{
    private readonly IShippingService _shippingService;

    public IndexModel(IRazorToStringRenderer razorToStringRenderer,
        IShippingService shippingService) : base(razorToStringRenderer)
    {
        _shippingService = shippingService;
    }

    public List<ShippingDto> Shippings { get; set; }

    public async Task<IActionResult> OnGet()
    {
        Shippings = await GetData(async () => await _shippingService.GetAll());
        return Page();
    }

    public async Task<IActionResult> OnPost(CreateShippingViewModel viewModel)
    {
        var result = await _shippingService.Create(viewModel);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxRedirectToPageResult();
    }

    public async Task<IActionResult> OnGetShowAddPage()
    {
        return await AjaxHtmlSuccessResultAsync("_Add", new CreateShippingViewModel());
    }

    public async Task<IActionResult> OnPostEditShipping(EditShippingViewModel model)
    {
        var result = await _shippingService.Edit(model);
        if (result.IsSuccessful == false)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxRedirectToPageResult();
    }

    public async Task<IActionResult> OnGetShowEditPage(long id)
    {
        var shipping = await GetData(async () => await _shippingService.GetById(id));
        if (shipping == null)
            return AjaxErrorMessageResult(ValidationMessages.FieldNotFound("روش ارسال"), ApiStatusCode.NotFound);

        var model = new EditShippingViewModel
        {
            Id = shipping.Id,
            Name = shipping.Name,
            Cost = shipping.Cost
        };
        return await AjaxHtmlSuccessResultAsync("_Edit", model);
    }

    public async Task<IActionResult> OnPostRemoveShipping(long id)
    {
        var result = await _shippingService.Remove(id);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxRedirectToPageResult();
    }
}