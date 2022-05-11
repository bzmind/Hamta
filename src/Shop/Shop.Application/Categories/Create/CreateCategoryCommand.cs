using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Domain.CategoryAggregate.Services;

namespace Shop.Application.Categories.Create;

public record CreateCategoryCommand(string Title, string Slug, Dictionary<string, string>? Specifications)
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
                specifications.Add(new CategorySpecification(category.Id, specification.Key, specification.Value)));

            category.SetSpecifications(specifications);
        }
        
        await _categoryRepository.SaveAsync();
        return OperationResult<long>.Success(category.Id);
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