namespace Domain.Category_Aggregate.Services;

public interface ICategoryDomainService
{
    bool IsDuplicateCategory(long categoryId);
    bool IsDuplicateSlug(string slug);
}