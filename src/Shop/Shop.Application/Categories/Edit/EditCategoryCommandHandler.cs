using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.CategoryAggregate.Repository;

namespace Shop.Application.Categories.Edit;

public record EditCategoryCommand(long Id, long? ParentId, string Title, string Slug,
    Dictionary<string, string>? Specifications) : IBaseCommand;

public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public EditCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetAsTrackingAsync(request.Id);

        if (category == null)
            return OperationResult.NotFound();

        if (request.Specifications != null && request.Specifications.Any())
        {
            var specifications = new List<CategorySpecification>();
            request.Specifications.ToList().ForEach(specification =>
                specifications.Add(new CategorySpecification(category.Id, specification.Key, specification.Value)));
            category.SetSpecifications(specifications);
        }
        
        await _categoryRepository.SaveAsync();
        return OperationResult.Success();
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