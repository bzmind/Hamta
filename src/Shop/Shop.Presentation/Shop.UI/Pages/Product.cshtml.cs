using Microsoft.AspNetCore.Mvc;
using Shop.UI.Services.Products;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages;

public class ProductModel : BaseRazorPage
{
    private readonly IProductService _productService;

    public ProductModel(IRazorToStringRenderer razorToStringRenderer,
        IProductService productService) : base(razorToStringRenderer)
    {
        _productService = productService;
    }

    public async Task<IActionResult> OnGet(string slug)
    {
        var product = await _productService.GetBySlug(slug);
        if (product == null)
            return NotFound();
        return Page();
    }
}