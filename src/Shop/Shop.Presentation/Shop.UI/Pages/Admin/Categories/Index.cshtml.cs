using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.UI.Services.Categories;
using Shop.UI.Setup.RazorUtility;
using Shop.UI.ViewModels.Categories;

namespace Shop.UI.Pages.Admin.Categories;

public class IndexModel : BaseRazorPage
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public IndexModel(ICategoryService categoryService,
        IRazorToStringRenderer razorToStringRenderer, IMapper mapper) : base(razorToStringRenderer)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    public List<CategoryViewModel> Categories { get; set; }

    public async Task OnGet()
    {
        var categoryDtos = await _categoryService.GetAll();
        var categories = new List<CategoryViewModel>();
        categoryDtos.ForEach(dto =>
        {
            categories.Add(_mapper.Map<CategoryViewModel>(dto));
        });
        Categories = categories;
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