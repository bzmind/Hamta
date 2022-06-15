using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Domain.CategoryAggregate.Services;

namespace Shop.Application.Categories.Create;

public record CreateCategoryCommand(string Title, string Slug, List<Specification>? Specifications)
    : IBaseCommand<long>;

public class CreateCategoryCommandHandler : IBaseCommandHandler<CreateCategoryCommand, long>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository,
        ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }

    public async Task<OperationResult<long>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(null, request.Title, request.Slug, _categoryDomainService);

        await _categoryRepository.AddAsync(category);

        if (request.Specifications != null && request.Specifications.Any())
        {
            var specifications = new List<CategorySpecification>();

            request.Specifications.ToList().ForEach(specification =>
                specifications.Add(new CategorySpecification(category.Id, specification.Title,
                    specification.Description, specification.IsImportantFeature)));

            category.SetSpecifications(specifications);
        }

        await _categoryRepository.SaveAsync();
        return OperationResult<long>.Success(category.Id);
    }
}

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
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
                .NotNull().WithMessage(ValidationMessages.FieldRequired("عنوان مشخصات"))
                .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عنوان مشخصات"));

            specification.RuleFor(spec => spec.Description)
                .NotNull().WithMessage(ValidationMessages.FieldRequired("توضیحات مشخصات"))
                .NotEmpty().WithMessage(ValidationMessages.FieldRequired("توضیحات مشخصات"));
        });
    }
}