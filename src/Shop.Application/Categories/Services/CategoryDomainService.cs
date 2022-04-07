using Shop.Domain.Category_Aggregate.Repository;
using Shop.Domain.Category_Aggregate.Services;

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

    public bool IsThirdCategory(long id)
    {
        throw new NotImplementedException();
    }
}