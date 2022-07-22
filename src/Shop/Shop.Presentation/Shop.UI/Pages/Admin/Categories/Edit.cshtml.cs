using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels;
using Shop.API.ViewModels.Categories;
using Shop.UI.Services.Categories;
using Shop.UI.Setup.ModelStateExtensions;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Admin.Categories;

[BindProperties]
public class EditModel : BaseRazorPage
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public EditModel(IRazorToStringRenderer razorToStringRenderer,
        ICategoryService categoryService, IMapper mapper) : base(razorToStringRenderer)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long Id { get; set; }

    public long? ParentId { get; set; }

    [DisplayName("عنوان")]
    [Required(ErrorMessage = ValidationMessages.TitleRequired)]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Title { get; set; }

    [DisplayName("اسلاگ")]
    [Required(ErrorMessage = ValidationMessages.TitleRequired)]
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Slug { get; set; }

    [DisplayName("مشخصات")]
    public List<SpecificationViewModel> Specifications { get; set; }

    public async Task<IActionResult> OnGet(long categoryId)
    {
        var category = await _categoryService.GetById(categoryId);
        if (category == null)
            return RedirectToPage("Index");

        Id = category.Id;
        ParentId = category.ParentId;
        Title = category.Title;
        Slug = category.Slug;
        List<SpecificationViewModel> specifications = new() { new() };
        if (category.Specifications.Any())
            specifications.Clear();
        category.Specifications.ForEach(dto =>
        {
            specifications.Add(_mapper.Map<SpecificationViewModel>(dto));
        });
        Specifications = specifications;
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _categoryService.Edit(new EditCategoryViewModel
        {
            Id = Id,
            ParentId = ParentId,
            Title = Title,
            Slug = Slug,
            Specifications = Specifications
        });
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return RedirectToPage("Edit", new { categoryId = Id }).WithModelStateOf(this);
        }
        return RedirectToPage("Index");
    }
}