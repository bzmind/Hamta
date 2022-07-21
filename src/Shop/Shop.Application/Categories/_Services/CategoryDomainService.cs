using Shop.Domain.CategoryAggregate.Repository;
using Shop.Domain.CategoryAggregate.Services;

namespace Shop.Application.Categories._Services;

public class CategoryDomainService : ICategoryDomainService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryDomainService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public bool IsDuplicateSlug(long id, string slug)
    {
        var category = _categoryRepository.GetCategoryBySlug(slug);
        if (category == null)
            return false;
        if (category.Id == id)
            return false;
        return true;
    }
}