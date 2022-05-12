using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Domain.CategoryAggregate.Services;

namespace Shop.Application.Categories.AddSubCategory;

public record AddSubCategoryCommand(long ParentId, string Title, string Slug,
    Dictionary<string, SpecificationDetails>? Specifications) : IBaseCommand<long>;

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
        var newSubCategory = new Category(request.ParentId, request.Title, request.Slug, _categoryDomainService);

        await _categoryRepository.AddAsync(newSubCategory);

        if (request.Specifications != null && request.Specifications.Any())
        {
            var specifications = new List<CategorySpecification>();

            request.Specifications.ToList().ForEach(specification => 
                specifications.Add(new CategorySpecification(newSubCategory.Id, specification.Key,
                    specification.Value.Description, specification.Value.IsImportantFeature)));

            newSubCategory.SetSpecifications(specifications);
        }

        var parentCategory = await _categoryRepository.GetAsTrackingAsync(request.ParentId);

        if (parentCategory == null)
            return OperationResult<long>.NotFound();

        parentCategory.AddSubCategory(newSubCategory);

        await _categoryRepository.SaveAsync();
        return OperationResult<long>.Success(newSubCategory.Id);
    }
}

internal class AddSubCategoryCommandValidator : AbstractValidator<AddSubCategoryCommand>
{
    public AddSubCategoryCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عنوان"));

        RuleFor(c => c.Slug)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("Slug"));

        RuleForEach(c => c.Specifications).ChildRules(specification =>
        {
            specification.RuleFor(spec => spec.Key)
                .NotNull()
                .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عنوان"));

            specification.RuleFor(spec => spec.Value)
                .NotNull()
                .NotEmpty().WithMessage(ValidationMessages.FieldRequired("توضیحات"));
        });
    }
}