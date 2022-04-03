namespace Shop.Domain.Category_Aggregate.Services;

public interface ICategoryDomainService
{
    bool IsDuplicateSlug(string slug);
}