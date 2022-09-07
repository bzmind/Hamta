using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Categories;
using Shop.UI.Services.Categories;
using Shop.UI.Setup.ModelStateExtensions;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Categories;

[BindProperties]
public class AddModel : BaseRazorPage
{
    private readonly ICategoryService _categoryService;

    public AddModel(IRazorToStringRenderer razorToStringRenderer,
        ICategoryService categoryService) : base(razorToStringRenderer)
    {
        _categoryService = categoryService;
    }

    public CreateCategoryViewModel CreateCategoryViewModel { get; set; } = new();

    public void OnGet()
    {
        CreateCategoryViewModel.Specifications.Add(new());
    }

    public async Task<IActionResult> OnPost(long? parentId)
    {
        if (parentId != null)
        {
            var addSubCategoryResult = await _categoryService.AddSubCategory(new AddSubCategoryViewModel
            {
                ParentId = parentId.Value,
                Title = CreateCategoryViewModel.Title,
                Slug = CreateCategoryViewModel.Slug,
                Specifications = CreateCategoryViewModel.Specifications
            });

            if (!addSubCategoryResult.IsSuccessful)
            {
                MakeAlert(addSubCategoryResult);
                return RedirectToPage("Add").WithModelStateOf(this);
            }
        }
        else
        {
            var result = await _categoryService.Create(new CreateCategoryViewModel
            {
                Title = CreateCategoryViewModel.Title,
                Slug = CreateCategoryViewModel.Slug,
                Specifications = CreateCategoryViewModel.Specifications
            });
            if (!result.IsSuccessful)
            {
                MakeAlert(result);
                return RedirectToPage("Add").WithModelStateOf(this);
            }
        }
        return RedirectToPage("Index");
    }
}