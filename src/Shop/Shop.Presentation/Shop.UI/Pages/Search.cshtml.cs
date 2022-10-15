using Microsoft.AspNetCore.Mvc;
using Shop.Query.Products._DTOs;
using Shop.UI.Services.Products;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages;

public class SearchModel : BaseRazorPage
{
    private readonly IProductService _productService;

    public SearchModel(IRazorToStringRenderer razorToStringRenderer,
        IProductService productService) : base(razorToStringRenderer)
    {
        _productService = productService;
    }

    [BindProperty(SupportsGet = true)]
    public ProductForShopFilterParams FilterParams { get; set; }

    public ProductForShopResult Products { get; set; }

    public async Task OnGet()
    {
        FilterParams.Take = 20;
        var result = await _productService.GetForShopByFilter(FilterParams);
        Products = result;
    }
}