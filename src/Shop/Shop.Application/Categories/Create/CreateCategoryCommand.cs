using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Application.Categories._DTOs;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Domain.CategoryAggregate.Services;

namespace Shop.Application.Categories.Create;

public record CreateCategoryCommand(string Title, string Slug,
    List<CategorySpecificationDto>? Specifications) : IBaseCommand<long>;

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
                    specification.IsImportantFeature)));

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