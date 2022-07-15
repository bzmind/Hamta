using AutoMapper;
using Common.Api;
using Common.Api.Utility;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Users.Addresses;
using Shop.Query.Users._DTOs;
using Shop.UI.Services.UserAddresses;
using Shop.UI.Setup.ModelStateExtensions;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Profile.Addresses;

[BindProperties]
public class IndexModel : BaseRazorPage
{
    private readonly IUserAddressService _userAddressService;
    private readonly IMapper _mapper;

    public IndexModel(IRazorToStringRenderer razorToStringRenderer,
        IUserAddressService userAddressService, IMapper mapper) : base(razorToStringRenderer)
    {
        _userAddressService = userAddressService;
        _mapper = mapper;
    }

    public List<UserAddressDto> Addresses { get; set; } = new();

    public async Task<IActionResult> OnGet()
    {
        Addresses = await _userAddressService.GetAll(User.GetUserId());
        return Page();
    }

    public async Task<IActionResult> OnPost(CreateUserAddressViewModel model)
    {
        var result = await _userAddressService.Create(model);
        return AjaxRedirectToPageResult("Index");
    }

    public async Task<IActionResult> OnGetShowAddPage()
    {
        return await AjaxSuccessHtmlResultAsync("_Add", new CreateUserAddressViewModel());
    }

    public async Task<IActionResult> OnGetShowEditPage(long addressId)
    {
        var address = await _userAddressService.GetById(addressId);
        if (address == null)
        {
            MakeAlert(ValidationMessages.FieldNotFound("آدرس"));
            return AjaxErrorMessageResult(ValidationMessages.FieldNotFound("آدرس"), ApiStatusCode.NotFound);
        }

        var model = _mapper.Map<EditUserAddressViewModel>(address);
        return await AjaxSuccessHtmlResultAsync("_Edit", model);
    }
    
    public async Task<IActionResult> OnPostEditAddress(EditUserAddressViewModel model)
    {
        var result = await _userAddressService.Edit(model);
        if (result.IsSuccessful == false)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxRedirectToPageResult("Index");
    }

    public async Task<IActionResult> OnPostRemoveAddress(long addressId)
    {
        var result = await _userAddressService.Remove(addressId);
        if (result.IsSuccessful == false)
        {
            MakeAlert(result);
            return RedirectToPage().WithModelStateOf(this);
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnGetActivateAddress(long addressId)
    {
        var result = await _userAddressService.Activate(addressId);
        if (result.IsSuccessful == false)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }

        return AjaxEmptySuccessResult();
    }
}