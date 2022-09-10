using AutoMapper;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shop.API.ViewModels.Products;
using Shop.Query.Products._DTOs;
using Shop.UI.Services.Categories;
using Shop.UI.Services.Products;
using Shop.UI.Setup.ModelStateExtensions;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Products;

[BindProperties]
public class EditModel : BaseRazorPage
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public EditModel(IRazorToStringRenderer razorToStringRenderer, IMapper mapper,
        IProductService productService, ICategoryService categoryService) : base(razorToStringRenderer)
    {
        _mapper = mapper;
        _productService = productService;
        _categoryService = categoryService;
    }

    public EditProductViewModel EditProductViewModel { get; set; } = new();

    [BindNever]
    public string MainImage { get; set; }

    [BindNever]
    public List<ProductGalleryImageDto> GalleryImages { get; set; } = new();

    public async Task<IActionResult> OnGet(long productId)
    {
        var product = await _productService.GetById(productId);
        if (product == null)
        {
            MakeAlert(ValidationMessages.FieldNotFound("محصول"));
            return RedirectToPage("Index");
        }
        EditProductViewModel = new EditProductViewModel
        {
            ProductId = product.Id,
            CategoryId = product.CategoryId,
            Name = product.Name,
            EnglishName = product.EnglishName,
            Slug = product.Slug,
            Introduction = product.Introduction,
            Review = product.Review,
            Specifications = product.Specifications.Any()
                ? _mapper.Map<List<ProductSpecificationViewModel>>(product.Specifications)
                : new() { new() }
        };
        MainImage = product.MainImage;
        GalleryImages = product.GalleryImages.Any() ? product.GalleryImages.ToList() : new() { new() };
        return Page();
    }

    public async Task<IActionResult> OnGetShowCategorySpecifications(long productId, long categoryId)
    {
        var product = await _productService.GetById(productId);
        var categorySpecifications = await _categoryService.GetSpecificationsByCategoryId(categoryId);
        if (product == null)
        {
            MakeAlert(ValidationMessages.FieldNotFound("محصول"));
            return AjaxRedirectToPageResult();
        }

        var productCategorySpecifications = new List<ProductCategorySpecificationViewModel>();
        categorySpecifications.ForEach(categorySpec =>
        {
            var productSpec = product.CategorySpecifications
                .FirstOrDefault(s => s.CategorySpecificationId == categorySpec.Id);
            productCategorySpecifications.Add(new ProductCategorySpecificationViewModel
            {
                CategorySpecificationId = categorySpec.Id,
                Title = categorySpec.Title,
                Description = productSpec == null ? "" : productSpec.Description,
                IsOptional = categorySpec.IsOptional,
                IsImportant = categorySpec.IsImportant
            });
        });
        return await AjaxHtmlSuccessResultAsync("_CategorySpecifications", productCategorySpecifications);
    }

    public async Task<IActionResult> OnPost(long productId)
    {
        EditProductViewModel.ProductId = productId;
        var result = await _productService.Edit(EditProductViewModel);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return RedirectToPage("Edit").WithModelStateOf(this);
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
        return AjaxDataSuccessResult(result);
    }
}