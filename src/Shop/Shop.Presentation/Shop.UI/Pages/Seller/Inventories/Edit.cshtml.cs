using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shop.API.ViewModels.Sellers.Inventories;
using Shop.UI.Services.Sellers;
using Shop.UI.Setup.ModelStateExtensions;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Seller.Inventories;

[BindProperties]
public class EditModel : BaseRazorPage
{
    private readonly ISellerService _sellerService;

    public EditModel(IRazorToStringRenderer razorToStringRenderer,
        ISellerService sellerService) : base(razorToStringRenderer)
    {
        _sellerService = sellerService;
    }

    public EditSellerInventoryViewModel EditInventoryViewModel { get; set; }

    [BindNever]
    public string ProductMainImage { get; set; }

    [BindNever]
    public string ProductName { get; set; }

    [BindNever]
    public string? ProductEnglishName { get; set; }

    public async Task<IActionResult> OnGet(long inventoryId)
    {
        var inventory = await GetData(async () => await _sellerService.GetInventoryById(inventoryId));
        if (inventory == null)
            return RedirectToPage("Index");

        EditInventoryViewModel = new EditSellerInventoryViewModel
        {
            InventoryId = inventory.Id,
            ProductId = inventory.ProductId,
            ColorId = inventory.ColorId,
            Price = inventory.Price,
            DiscountPercentage = inventory.DiscountPercentage,
            Quantity = inventory.Quantity
        };
        ProductMainImage = inventory.ProductMainImage;
        ProductName = inventory.ProductName;
        ProductEnglishName = inventory.ProductEnglishName;
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _sellerService.EditInventory(EditInventoryViewModel);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            RedirectToPage("Edit").WithModelStateOf(this);
        }

        return RedirectToPage("Index");
    }
}