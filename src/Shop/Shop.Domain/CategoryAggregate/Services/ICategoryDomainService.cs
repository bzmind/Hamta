namespace Shop.Domain.CategoryAggregate.Services;

public interface ICategoryDomainService
{
    bool IsDuplicateSlug(string slug);
}