﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Query.Categories._DTOs;
using Shop.UI.Services.Categories;
using Shop.UI.Setup.RazorUtility;

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