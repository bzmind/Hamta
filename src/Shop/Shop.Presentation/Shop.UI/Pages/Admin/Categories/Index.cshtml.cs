using Microsoft.AspNetCore.Mvc;
using Shop.Query.Categories._DTOs;
using Shop.UI.Services.Categories;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Categories;

public class IndexModel : BaseRazorPage
{
    private readonly ICategoryService _categoryService;

    public IndexModel(ICategoryService categoryService,
        IRazorToStringRenderer razorToStringRenderer) : base(razorToStringRenderer)
    {
        _categoryService = categoryService;
    }

    public List<CategoryDto> Categories { get; set; }

    public async Task OnGet()
    {
        Categories = await _categoryService.GetAll();
    }

    public async Task<IActionResult> OnPostRemoveCategory(long categoryId)
    {
        var result = await _categoryService.Remove(categoryId);
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return AjaxErrorMessageResult(result);
        }
        return AjaxRedirectToPageResult();
    }
}