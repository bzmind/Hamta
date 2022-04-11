using Common.Domain.Exceptions;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Domain.CategoryAggregate.Services;

namespace Shop.Application.Categories.Services;

public class CategoryDomainService : ICategoryDomainService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryDomainService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public bool IsDuplicateSlug(string slug)
    {
        return _categoryRepository.Exists(c => c.Slug == slug);
    }

    public bool IsThirdCategory(long categoryId)
    {
        var category = _categoryRepository.Get(categoryId);

        if (category == null)
            throw new DataNotFoundInDatabaseDomainException("No such category was found");

        if (category.SubCategories.Any())
            return false;

        var firstParentCategory = GetCategoryParent(category);
        if (firstParentCategory == null)
            return false;

        var secondParentCategory = GetCategoryParent(firstParentCategory);
        if (secondParentCategory == null)
            return false;

        return true;
    }

    private Category? GetCategoryParent(Category category)
    {
        if (category.ParentId == null)
            return null;

        var parentCategory = _categoryRepository.Get(category.ParentId.Value);

        if (parentCategory == null)
            throw new DataNotFoundInDatabaseDomainException("No such parent category was found");

        return parentCategory;
    }
}