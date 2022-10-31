using Common.Api.Utility;
using Microsoft.AspNetCore.Mvc;
using Shop.Query.Sellers._DTOs;
using Shop.UI.Services.Sellers;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Seller.Inventories;

[BindProperties]
public class IndexModel : BaseRazorPage
{
    private readonly ISellerService _sellerService;

    public IndexModel(IRazorToStringRenderer razorToStringRenderer,
        ISellerService sellerService) : base(razorToStringRenderer)
    {
        _sellerService = sellerService;
    }

    public SellerDto Seller { get; set; }

    [BindProperty(SupportsGet = true)]
    public SellerInventoryFilterParams FilterParams { get; set; }

    public SellerInventoryFilterResult FilterResult { get; set; }

    public async Task OnGet()
    {
        Seller = await GetData(async () => await _sellerService.GetCurrentSeller());
        FilterParams.UserId = User.GetUserId();
        FilterResult = await GetData(async () => await _sellerService.GetInventoryByFilter(FilterParams));
    }

    public async Task<IActionResult> OnPostRemoveInventory(long inventoryId)
    {
        var result = await _sellerService.RemoveInventory(inventoryId);
        MakeAlert(result);
        if (!result.IsSuccessful)
            return AjaxErrorMessageResult(result);
        return AjaxRedirectToPageResult();
    }
}