namespace Shop.Domain.CategoryAggregate.Services;

public interface ICategoryDomainService
{
    bool IsDuplicateSlug(long id, string slug);
}