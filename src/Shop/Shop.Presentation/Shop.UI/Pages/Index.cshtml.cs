using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.UI.Services.Products;

namespace Shop.UI.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IProductService _productService;

    public IndexModel(ILogger<IndexModel> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    public async Task OnGet()
    {
        //var result = await _productService.Create(new CreateProductCommandViewModel
        //{
        //    CategoryId = 1,
        //    Name = "test",
        //    EnglishName = "test",
        //    Description = "test",
        //    Slug = "testtt",
        //    GalleryImages = new List<IFormFile>(),
        //    CustomSpecifications = new List<Specification>
        //    {
        //        new Specification
        //        {
        //            Title = "s",
        //            Description = "s",
        //            IsImportantFeature = true
        //        }
        //    },
        //    ExtraDescription = new Dictionary<string, string>
        //    {
        //        {"d1", "d1"},
        //        {"d2", "d2"}
        //    }
        //});
    }
}