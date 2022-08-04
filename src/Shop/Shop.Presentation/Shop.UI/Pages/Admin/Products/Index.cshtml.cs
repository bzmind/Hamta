using Microsoft.AspNetCore.Mvc;
using Shop.Query.Products._DTOs;
using Shop.UI.Services.Products;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Products;

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

    public ProductFilterResult FilterResult { get; set; }

    public async Task OnGet()
    {
        FilterResult = await _productService.GetByFilter(FilterParams);
    }
}