using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Domain.CategoryAggregate.Services;

namespace Shop.Application.Categories.Edit;

public record EditCategoryCommand(long Id, long? ParentId, string Title, string Slug,
    List<SpecificationDto>? Specifications) : IBaseCommand;

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

        category.Edit(request.ParentId, request.Title, request.Slug, _categoryDomainService);

        var specifications = new List<CategorySpecification>();
        request.Specifications.ToList().ForEach(specification =>
            specifications.Add(new CategorySpecification(category.Id, specification.Title,
                specification.Description, specification.IsImportantFeature)));
        category.SetSpecifications(specifications);

        await _categoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
{
    public EditCategoryCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("عنوان"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عنوان"));

        RuleFor(c => c.Slug)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("اسلاگ"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("اسلاگ"));

        RuleForEach(c => c.Specifications).ChildRules(specification =>
        {
            specification.RuleFor(spec => spec.Title)
                .NotNull().WithMessage(ValidationMessages.FieldRequired("عنوان"))
                .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عنوان"));

            specification.RuleFor(spec => spec.Description)
                .NotNull().WithMessage(ValidationMessages.FieldRequired("توضیحات"))
                .NotEmpty().WithMessage(ValidationMessages.FieldRequired("توضیحات"));
        });
    }
}