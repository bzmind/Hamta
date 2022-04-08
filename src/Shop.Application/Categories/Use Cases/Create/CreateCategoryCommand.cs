using Common.Application;
using Common.Application.Base_Classes;
using Common.Application.Utility;
using FluentValidation;
using MediatR;
using Shop.Domain.Category_Aggregate;
using Shop.Domain.Category_Aggregate.Repository;
using Shop.Domain.Category_Aggregate.Services;

namespace Shop.Application.Categories.Use_Cases.Create;

public record CreateCategoryCommand(long? ParentId, string Title, string Slug,
    List<Category> SubCategories, List<CategorySpecification> Specifications) : IBaseCommand;

public class CreateCategoryCommandHandler : IBaseCommandHandler<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository,
        ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }

    public async Task<OperationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.ParentId, request.Title, request.Slug, _categoryDomainService);

        // For testing purposes
        //category.SubCategories.Add(new Category(1, "ss", "sds", _categoryDomainService));

        if (request.Specifications.Count > 0)
        {
            var specifications = new List<CategorySpecification>();
            request.Specifications.ForEach(specification =>
                specifications.Add(new CategorySpecification(specification.CategoryId, specification.Title,
                    specification.Description)));
            category.SetSpecifications(specifications);
        }

        if (request.SubCategories.Count > 0)
        {
            var subCategories = new List<Category>();
            request.SubCategories.ForEach(subCategory =>
                subCategories.Add(new Category(subCategory.ParentId, subCategory.Title, subCategory.Slug,
                    _categoryDomainService)));
            category.SetSubCategories(subCategories);
        }

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
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