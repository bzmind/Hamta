using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Products;
using Shop.UI.Services.Categories;
using Shop.UI.Services.Products;
using Shop.UI.Setup.ModelStateExtensions;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Products;

[BindProperties]
public class AddModel : BaseRazorPage
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    public AddModel(ICategoryService categoryService,
        IRazorToStringRenderer razorToStringRenderer, IProductService productService) : base(razorToStringRenderer)
    {
        _categoryService = categoryService;
        _productService = productService;
    }

    public CreateProductViewModel CreateProductViewModel { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnGetShowCategorySpecifications(long categoryId)
    {
        var categorySpecifications = await _categoryService.GetSpecificationsByCategoryId(categoryId);
        var productCategorySpecifications = new List<ProductCategorySpecificationViewModel>();
        categorySpecifications.ForEach(categorySpec =>
        {
            productCategorySpecifications.Add(new ProductCategorySpecificationViewModel
            {
                CategorySpecificationId = categorySpec.Id,
                Title = categorySpec.Title,
                Description = "",
                IsOptional = categorySpec.IsOptional,
                IsImportant = categorySpec.IsImportant
            });
        });
        return await AjaxHtmlSuccessResultAsync("_CategorySpecifications", productCategorySpecifications);
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _productService.Create(CreateProductViewModel);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return RedirectToPage("Add").WithModelStateOf(this);
        }
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostAddReviewImage(IFormFile image)
    {
        var result = await _productService.AddReviewImage(new AddProductReviewImageViewModel { Image = image });
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxSuccessResult(result);
    }
}