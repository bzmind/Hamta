using Common.Application.Utility;
using FluentValidation;
using MediatR;
using Shop.Domain.Category_Aggregate;
using Shop.Domain.Category_Aggregate.Repository;
using Shop.Domain.Category_Aggregate.Services;

namespace Shop.Application.Categories.Use_Cases.Edit;

public record EditCategoryCommand(long Id, long? ParentId, string Title, string Slug,
    List<Category> SubCategories, List<CategorySpecification> Specifications) : IRequest;

public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;

    public EditCategoryCommandHandler(ICategoryRepository categoryRepository,
        ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }

    public async Task<Unit> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetAsTrackingAsync(request.Id);

        if (category == null)
            return Unit.Value;
        
        var specifications = new List<CategorySpecification>();
        request.Specifications.ForEach(specification =>
            specifications.Add(new CategorySpecification(specification.CategoryId, specification.Title,
                specification.Description)));
        category.SetSpecifications(specifications);

        var subCategories = new List<Category>();
        request.SubCategories.ForEach(subCategory =>
            subCategories.Add(new Category(subCategory.ParentId, subCategory.Title, subCategory.Slug,
                _categoryDomainService)));
        category.SetSubCategories(subCategories);

        await _categoryRepository.SaveAsync();
        return Unit.Value;
    }
}

internal class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
{
    public EditCategoryCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عنوان"));

        RuleFor(c => c.Slug)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("Slug"));

        RuleForEach(c => c.SubCategories).ChildRules(subCategory =>
        {
            subCategory.RuleFor(sc => sc.Title)
                .NotNull()
                .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عنوان"));

            subCategory.RuleFor(sc => sc.Slug)
                .NotNull()
                .NotEmpty().WithMessage(ValidationMessages.FieldRequired("Slug"));
        });

        RuleForEach(c => c.Specifications).ChildRules(specification =>
        {
            specification.RuleFor(spec => spec.Title)
                .NotNull()
                .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عنوان"));

            specification.RuleFor(spec => spec.Description)
                .NotNull()
                .NotEmpty().WithMessage(ValidationMessages.FieldRequired("توضیحات"));
        });
    }
}