﻿using Common.Api;
using Shop.Query.Categories._DTOs;
using Shop.UI.Models.Categories;

namespace Shop.UI.Services.Categories;

public interface ICategoryService
{
    Task<ApiResult?> Create(CreateCategoryViewModel model);
    Task<ApiResult?> AddSubCategory(AddSubCategoryViewModel model);
    Task<ApiResult?> Edit(EditCategoryViewModel model);
    Task<ApiResult?> Remove(long categoryId);

    Task<CategoryDto?> GetById(long categoryId);
    Task<List<CategoryDto>?> GetByParentId(long parentId);
    Task<List<CategoryDto>?> GetAll();
}