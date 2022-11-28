using Microsoft.AspNetCore.Mvc;
using Shop.Query.Products._DTOs;
using Shop.UI.Services.Products;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Products;

[BindProperties]
public class IndexModel : BaseRazorPage
{
    private readonly IProductService _productService;

    public IndexModel(IProductService productService,
        IRazorToStringRenderer razorToStringRenderer) : base(razorToStringRenderer)
    {
        _productService = productService;
    }

    [BindProperty(SupportsGet = true)]
    public ProductFilterParams FilterParams { get; set; }

    public ProductFilterResult Products { get; set; }

    public async Task OnGet()
    {
        Products = await GetData(async () => await _productService.GetByFilter(FilterParams));
    }

    public async Task<IActionResult> OnPostRemoveProduct(long productId)
    {
        var result = await _productService.Remove(productId);
        MakeAlert(result);
        return AjaxRedirectToPageResult();
    }
}