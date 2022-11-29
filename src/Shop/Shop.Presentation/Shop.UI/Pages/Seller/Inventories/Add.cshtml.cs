using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Sellers.Inventories;
using Shop.Query.Products._DTOs;
using Shop.UI.Services.Products;
using Shop.UI.Services.Sellers;
using Shop.UI.Setup.ModelStateExtensions;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Seller.Inventories;

[BindProperties]
public class AddModel : BaseRazorPage
{
    private readonly IProductService _productService;
    private readonly ISellerService _sellerService;

    public AddModel(IRazorToStringRenderer razorToStringRenderer, IProductService productService,
        ISellerService sellerService) : base(razorToStringRenderer)
    {
        _productService = productService;
        _sellerService = sellerService;
    }

    [BindProperty(SupportsGet = true)]
    public ProductFilterParams FilterParams { get; set; }

    public ProductFilterResult Products { get; set; }

    public AddSellerInventoryViewModel AddInventoryViewModel { get; set; }

    public async Task OnGet()
    {
        Products = await GetData(async () => await _productService.GetByFilter(FilterParams));
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _sellerService.AddInventory(AddInventoryViewModel);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return RedirectToPage("Add").WithModelStateOf(this);
        }

        return RedirectToPage("Index");
    }
}