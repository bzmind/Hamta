﻿using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.CategoryAggregate.Repository;

namespace Shop.Application.Categories.RemoveCategory;

public record RemoveCategoryCommand(long SubCategoryId) : IBaseCommand;

public class RemoveCategoryCommandHandler : IBaseCommandHandler<RemoveCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public RemoveCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<OperationResult> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.RemoveCategory(request.SubCategoryId);

        if (!category)
            return OperationResult.Error("امکان حذف این دسته بندی وجود ندارد");

        await _categoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}