using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels;
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

    [DisplayName("عنوان")]
    [Required(ErrorMessage = ValidationMessages.TitleRequired)]
    public string Title { get; set; }

    [DisplayName("اسلاگ")]
    [Required(ErrorMessage = ValidationMessages.TitleRequired)]
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Slug { get; set; }

    [DisplayName("مشخصات")]
    public List<SpecificationViewModel> Specifications { get; set; } = new() { new() };

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost([FromRoute] long? parentId)
    {
        if (parentId != null)
        {
            var addSubCategoryResult = await _categoryService.AddSubCategory(new AddSubCategoryViewModel
            {
                ParentId = parentId.Value,
                Title = Title,
                Slug = Slug,
                Specifications = Specifications
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
                Title = Title,
                Slug = Slug,
                Specifications = Specifications
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