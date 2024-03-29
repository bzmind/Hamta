﻿using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Application.Categories._DTOs;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Domain.CategoryAggregate.Services;

namespace Shop.Application.Categories.AddSubCategory;

public record AddSubCategoryCommand(long ParentId, string Title, string Slug, bool ShowInMenu,
    List<CategorySpecificationDto>? Specifications) : IBaseCommand<long>;

public class AddSubCategoryCommandHandler : IBaseCommandHandler<AddSubCategoryCommand, long>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;

    public AddSubCategoryCommandHandler(ICategoryRepository categoryRepository,
        ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }

    public async Task<OperationResult<long>> Handle(AddSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var newSubCategory = new Category(request.ParentId, request.Title, request.Slug, request.ShowInMenu,
            _categoryDomainService);

        await _categoryRepository.AddAsync(newSubCategory);

        var specifications = new List<CategorySpecification>();

        request.Specifications.ToList().ForEach(specification =>
            specifications.Add(new CategorySpecification(newSubCategory.Id, specification.Title,
                specification.IsImportant, specification.IsOptional, specification.IsFilterable)));

        newSubCategory.SetSpecifications(specifications);

        var parentCategory = await _categoryRepository.GetAsTrackingAsync(request.ParentId);

        if (parentCategory == null)
            return OperationResult<long>.NotFound();

        parentCategory.AddSubCategory(newSubCategory);

        await _categoryRepository.SaveAsync();
        return OperationResult<long>.Success(newSubCategory.Id);
    }
}

public class AddSubCategoryCommandValidator : AbstractValidator<AddSubCategoryCommand>
{
    public AddSubCategoryCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotNull().WithMessage(ValidationMessages.TitleRequired)
            .NotEmpty().WithMessage(ValidationMessages.TitleRequired)
            .MaximumLength(30).WithMessage(ValidationMessages.FieldCharactersMaxLength("عنوان", 30));

        RuleFor(c => c.Slug)
            .NotNull().WithMessage(ValidationMessages.SlugRequired)
            .NotEmpty().WithMessage(ValidationMessages.SlugRequired)
            .MaximumLength(100).WithMessage(ValidationMessages.FieldCharactersMaxLength("عنوان", 100));

        RuleForEach(c => c.Specifications).ChildRules(specification =>
        {
            specification.RuleFor(spec => spec.Title)
                .NotNull().WithMessage(ValidationMessages.TitleRequired)
                .NotEmpty().WithMessage(ValidationMessages.TitleRequired)
                .MaximumLength(50).WithMessage(ValidationMessages.FieldCharactersMaxLength("عنوان", 50));
        });
    }
}