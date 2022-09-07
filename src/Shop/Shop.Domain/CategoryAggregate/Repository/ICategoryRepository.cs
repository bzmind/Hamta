using Common.Domain.Repository;

namespace Shop.Domain.CategoryAggregate.Repository;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Category? GetCategoryBySlug(string slug);
    Task<List<CategorySpecification>> GetCategoryAndParentsSpecifications(long categoryId);
    Task<bool> RemoveCategory(long categoryId);
}