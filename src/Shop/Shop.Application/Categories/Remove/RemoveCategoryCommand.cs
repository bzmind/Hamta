using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Shop.Domain.CategoryAggregate.Repository;

namespace Shop.Application.Categories.Remove;

public record RemoveCategoryCommand(long SubCategoryId) : IBaseCommand;

public class RemoveCategoryCommandHandler : IBaseCommandHandler<RemoveCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public RemoveCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<OperationResult> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.RemoveCategory(request.SubCategoryId);

        if (!category)
            return OperationResult.Error(ValidationMessages.FieldCantBeRemoved("دسته بندی"));

        await _categoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}