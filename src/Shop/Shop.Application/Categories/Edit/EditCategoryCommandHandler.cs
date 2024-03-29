﻿using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Application.Categories._DTOs;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Domain.CategoryAggregate.Services;

namespace Shop.Application.Categories.Edit;

public record EditCategoryCommand(long Id, long? ParentId, string Title, string Slug, bool ShowInMenu,
    List<CategorySpecificationDto>? Specifications) : IBaseCommand;

public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;

    public EditCategoryCommandHandler(ICategoryRepository categoryRepository,
        ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }

    public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetAsTrackingAsync(request.Id);

        if (category == null)
            return OperationResult.NotFound();

        category.Edit(request.ParentId, request.Title, request.Slug, request.ShowInMenu, _categoryDomainService);

        if (request.Specifications != null && request.Specifications.Any())
        {
            request.Specifications.ToList().ForEach(specification =>
            category.EditSpecification(specification.Id, specification.Title, specification.IsImportant,
                specification.IsOptional, specification.IsFilterable,
                request.Specifications.Select(spec => spec.Id).ToList()));
        }

        await _categoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
{
    public EditCategoryCommandValidator()
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