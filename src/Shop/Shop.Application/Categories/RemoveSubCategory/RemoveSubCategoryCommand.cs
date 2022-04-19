using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.CategoryAggregate.Repository;

namespace Shop.Application.Categories.RemoveSubCategory;

public record RemoveSubCategoryCommand(long ParentCategoryId, long SubCategoryId) : IBaseCommand;

public class RemoveSubCategoryCommandHandler : IBaseCommandHandler<RemoveSubCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public RemoveSubCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<OperationResult> Handle(RemoveSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var parentCategory = await _categoryRepository.GetAsTrackingAsync(request.ParentCategoryId);

        if (parentCategory == null)
            return OperationResult.NotFound();

        parentCategory.RemoveSubCategory(request.SubCategoryId);

        await _categoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}