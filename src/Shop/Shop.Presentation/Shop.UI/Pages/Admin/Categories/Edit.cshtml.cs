﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

    public EditCategoryViewModel EditCategoryViewModel { get; set; } = new();

    public async Task<IActionResult> OnGet(long categoryId)
    {
        var category = await GetData(async () => await _categoryService.GetById(categoryId));
        if (category == null)
            return RedirectToPage("Index");

        EditCategoryViewModel.Id = category.Id;
        EditCategoryViewModel.ParentId = category.ParentId;
        EditCategoryViewModel.Title = category.Title;
        EditCategoryViewModel.Slug = category.Slug;
        EditCategoryViewModel.ShowInMenu = category.ShowInMenu;
        EditCategoryViewModel.Specifications = category.Specifications.Any()
            ? _mapper.Map<List<CategorySpecificationViewModel>>(category.Specifications)
            : new() { new() };
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _categoryService.Edit(new EditCategoryViewModel
        {
            Id = EditCategoryViewModel.Id,
            ParentId = EditCategoryViewModel.ParentId,
            Title = EditCategoryViewModel.Title,
            Slug = EditCategoryViewModel.Slug,
            ShowInMenu = EditCategoryViewModel.ShowInMenu,
            Specifications = EditCategoryViewModel.Specifications
        });
        if (!result.IsSuccessful)
        {
            MakeAlert(result);
            return RedirectToPage("Edit", new { categoryId = EditCategoryViewModel.Id }).WithModelStateOf(this);
        }
        return RedirectToPage("Index");
    }
}